        private class LegacyGlyphStore : ITexturedGlyphLookupStore
        {
            private readonly ISkin skin;

            public LegacyGlyphStore(ISkin skin)
            {
                this.skin = skin;
            }

            public ITexturedCharacterGlyph Get(string fontName, char character)
            {
                var lookup = getLookupName(character);

                var texture = skin.GetTexture($"{fontName}-{lookup}");

                if (texture == null)
                    return null;

                return new TexturedCharacterGlyph(new CharacterGlyph(character, 0, 0, texture.Width, null), texture, 1f / texture.ScaleAdjust);
            }

            private static string getLookupName(char character)
            {
                switch (character)
                {
                    case ',':
                        return "comma";

                    case '.':
                        return "dot";

                    case '%':
                        return "percent";

                    default:
                        return character.ToString();
                }
            }

            public Task<ITexturedCharacterGlyph> GetAsync(string fontName, char character) => Task.Run(() => Get(fontName, character));
        }