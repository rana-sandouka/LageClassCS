        private class ItemContent : CircularContainer
        {
            public readonly Bindable<bool> IsCreated = new Bindable<bool>();

            private readonly IBindable<string> collectionName;
            private readonly BeatmapCollection collection;

            [Resolved(CanBeNull = true)]
            private CollectionManager collectionManager { get; set; }

            private Container textBoxPaddingContainer;
            private ItemTextBox textBox;

            public ItemContent(BeatmapCollection collection)
            {
                this.collection = collection;

                RelativeSizeAxes = Axes.X;
                Height = item_height;
                Masking = true;

                collectionName = collection.Name.GetBoundCopy();
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                Children = new Drawable[]
                {
                    new DeleteButton(collection)
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        IsCreated = { BindTarget = IsCreated },
                        IsTextBoxHovered = v => textBox.ReceivePositionalInputAt(v)
                    },
                    textBoxPaddingContainer = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding { Right = button_width },
                        Children = new Drawable[]
                        {
                            textBox = new ItemTextBox
                            {
                                RelativeSizeAxes = Axes.Both,
                                Size = Vector2.One,
                                CornerRadius = item_height / 2,
                                Current = collection.Name,
                                PlaceholderText = IsCreated.Value ? string.Empty : "Create a new collection"
                            },
                        }
                    },
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                collectionName.BindValueChanged(_ => createNewCollection(), true);
                IsCreated.BindValueChanged(created => textBoxPaddingContainer.Padding = new MarginPadding { Right = created.NewValue ? button_width : 0 }, true);
            }

            private void createNewCollection()
            {
                if (IsCreated.Value)
                    return;

                if (string.IsNullOrEmpty(collectionName.Value))
                    return;

                // Add the new collection and disable our placeholder. If all text is removed, the placeholder should not show back again.
                collectionManager?.Collections.Add(collection);
                textBox.PlaceholderText = string.Empty;

                // When this item changes from placeholder to non-placeholder (via changing containers), its textbox will lose focus, so it needs to be re-focused.
                Schedule(() => GetContainingInputManager().ChangeFocus(textBox));

                IsCreated.Value = true;
            }
        }