        private class JoinVisualiser : CompositeDrawable
        {
            private readonly Container<DrawableTournamentMatch> matchesContainer;
            public readonly TournamentMatch Source;
            private readonly bool losers;
            private readonly Action complete;

            private ProgressionPath path;

            public JoinVisualiser(Container<DrawableTournamentMatch> matchesContainer, TournamentMatch source, bool losers, Action complete)
            {
                this.matchesContainer = matchesContainer;
                RelativeSizeAxes = Axes.Both;

                Source = source;
                this.losers = losers;
                this.complete = complete;
                if (losers)
                    Source.LosersProgression.Value = null;
                else
                    Source.Progression.Value = null;
            }

            private DrawableTournamentMatch findTarget(InputState state)
            {
                return matchesContainer.FirstOrDefault(d => d.ReceivePositionalInputAt(state.Mouse.Position));
            }

            public override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
            {
                return true;
            }

            protected override bool OnMouseMove(MouseMoveEvent e)
            {
                var found = findTarget(e.CurrentState);

                if (found == path?.Destination)
                    return false;

                path?.Expire();
                path = null;

                if (found == null)
                    return false;

                AddInternal(path = new ProgressionPath(matchesContainer.First(c => c.Match == Source), found)
                {
                    Colour = Color4.Yellow,
                });

                return base.OnMouseMove(e);
            }

            protected override bool OnClick(ClickEvent e)
            {
                var found = findTarget(e.CurrentState);

                if (found != null)
                {
                    if (found.Match != Source)
                    {
                        if (losers)
                            Source.LosersProgression.Value = found.Match;
                        else
                            Source.Progression.Value = found.Match;
                    }

                    complete?.Invoke();
                    Expire();
                    return true;
                }

                return false;
            }
        }