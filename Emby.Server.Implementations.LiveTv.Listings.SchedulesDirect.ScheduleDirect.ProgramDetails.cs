            public class ProgramDetails
            {
                public string audience { get; set; }

                public string programID { get; set; }

                public List<Title> titles { get; set; }

                public EventDetails eventDetails { get; set; }

                public DescriptionsProgram descriptions { get; set; }

                public string originalAirDate { get; set; }

                public List<string> genres { get; set; }

                public string episodeTitle150 { get; set; }

                public List<MetadataPrograms> metadata { get; set; }

                public List<ContentRating> contentRating { get; set; }

                public List<Cast> cast { get; set; }

                public List<Crew> crew { get; set; }

                public string entityType { get; set; }

                public string showType { get; set; }

                public bool hasImageArtwork { get; set; }

                public string primaryImage { get; set; }

                public string thumbImage { get; set; }

                public string backdropImage { get; set; }

                public string bannerImage { get; set; }

                public string imageID { get; set; }

                public string md5 { get; set; }

                public List<string> contentAdvisory { get; set; }

                public Movie movie { get; set; }

                public List<Recommendation> recommendations { get; set; }
            }