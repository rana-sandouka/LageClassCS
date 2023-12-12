        private class TotalScoreBindable : Bindable<long>
        {
            public readonly Bindable<ScoringMode> ScoringMode = new Bindable<ScoringMode>();

            private readonly ScoreInfo score;
            private readonly Func<BeatmapDifficultyCache> difficulties;

            /// <summary>
            /// Creates a new <see cref="TotalScoreBindable"/>.
            /// </summary>
            /// <param name="score">The <see cref="ScoreInfo"/> to provide the total score of.</param>
            /// <param name="difficulties">A function to retrieve the <see cref="BeatmapDifficultyCache"/>.</param>
            public TotalScoreBindable(ScoreInfo score, Func<BeatmapDifficultyCache> difficulties)
            {
                this.score = score;
                this.difficulties = difficulties;

                ScoringMode.BindValueChanged(onScoringModeChanged, true);
            }

            private IBindable<StarDifficulty> difficultyBindable;
            private CancellationTokenSource difficultyCancellationSource;

            private void onScoringModeChanged(ValueChangedEvent<ScoringMode> mode)
            {
                difficultyCancellationSource?.Cancel();
                difficultyCancellationSource = null;

                if (score.Beatmap == null)
                {
                    Value = score.TotalScore;
                    return;
                }

                int beatmapMaxCombo;

                if (score.IsLegacyScore)
                {
                    // This score is guaranteed to be an osu!stable score.
                    // The combo must be determined through either the beatmap's max combo value or the difficulty calculator, as lazer's scoring has changed and the score statistics cannot be used.
                    if (score.Beatmap.MaxCombo == null)
                    {
                        if (score.Beatmap.ID == 0 || difficulties == null)
                        {
                            // We don't have enough information (max combo) to compute the score, so use the provided score.
                            Value = score.TotalScore;
                            return;
                        }

                        // We can compute the max combo locally after the async beatmap difficulty computation.
                        difficultyBindable = difficulties().GetBindableDifficulty(score.Beatmap, score.Ruleset, score.Mods, (difficultyCancellationSource = new CancellationTokenSource()).Token);
                        difficultyBindable.BindValueChanged(d => updateScore(d.NewValue.MaxCombo), true);

                        return;
                    }

                    beatmapMaxCombo = score.Beatmap.MaxCombo.Value;
                }
                else
                {
                    // This score is guaranteed to be an osu!lazer score.
                    // The combo must be determined through the score's statistics, as both the beatmap's max combo and the difficulty calculator will provide osu!stable combo values.
                    beatmapMaxCombo = Enum.GetValues(typeof(HitResult)).OfType<HitResult>().Where(r => r.AffectsCombo()).Select(r => score.Statistics.GetOrDefault(r)).Sum();
                }

                updateScore(beatmapMaxCombo);
            }

            private void updateScore(int beatmapMaxCombo)
            {
                if (beatmapMaxCombo == 0)
                {
                    Value = 0;
                    return;
                }

                var ruleset = score.Ruleset.CreateInstance();
                var scoreProcessor = ruleset.CreateScoreProcessor();

                scoreProcessor.Mods.Value = score.Mods;

                Value = (long)Math.Round(scoreProcessor.GetScore(ScoringMode.Value, beatmapMaxCombo, score.Accuracy, (double)score.MaxCombo / beatmapMaxCombo, score.Statistics));
            }
        }