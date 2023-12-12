    public class AvaloniaModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Locator

            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>().InstancePerLifetimeScope();

            // Core

            builder.RegisterType<ProjectEditor>().As<ProjectEditor>().InstancePerLifetimeScope();
            builder.RegisterType<StyleEditor>().As<StyleEditor>().InstancePerLifetimeScope();
            builder.RegisterType<Factory>().As<IFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ContainerFactory>().As<IContainerFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ShapeFactory>().As<IShapeFactory>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IEditorTool).GetTypeInfo().Assembly).As<IEditorTool>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IPathTool).GetTypeInfo().Assembly).As<IPathTool>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<HitTest>().As<IHitTest>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IBounds).GetTypeInfo().Assembly).As<IBounds>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataFlow>().As<DataFlow>().InstancePerLifetimeScope();

            // Dependencies

#if USE_SKIA
            builder.RegisterType<SkiaSharpRenderer>().As<IShapeRenderer>().InstancePerDependency();
#else
            builder.RegisterType<AvaloniaRenderer>().As<IShapeRenderer>().InstancePerDependency();
#endif
            builder.RegisterType<AvaloniaTextClipboard>().As<ITextClipboard>().InstancePerLifetimeScope();
            builder.RegisterType<TraceLog>().As<ILog>().SingleInstance();
            builder.RegisterType<DotNetFileSystem>().As<IFileSystem>().InstancePerLifetimeScope();
            builder.RegisterType<RoslynScriptRunner>().As<IScriptRunner>().InstancePerLifetimeScope();
            builder.RegisterType<NewtonsoftJsonSerializer>().As<IJsonSerializer>().InstancePerLifetimeScope();
            builder.RegisterType<PdfSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<SvgSvgWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<DrawingGroupXamlWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<PdfSkiaSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<DxfWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<SvgSkiaSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<PngSkiaSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<EmfWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<JpegSkiaSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<WebpSkiaSharpWriter>().As<IFileWriter>().InstancePerLifetimeScope();
            builder.RegisterType<OpenXmlReader>().As<ITextFieldReader<Database>>().InstancePerLifetimeScope();
            builder.RegisterType<CsvHelperReader>().As<ITextFieldReader<Database>>().InstancePerLifetimeScope();
            builder.RegisterType<OpenXmlWriter>().As<ITextFieldWriter<Database>>().InstancePerLifetimeScope();
            builder.RegisterType<CsvHelperWriter>().As<ITextFieldWriter<Database>>().InstancePerLifetimeScope();
            builder.RegisterType<SkiaSharpPathConverter>().As<IPathConverter>().InstancePerLifetimeScope();
            builder.RegisterType<SkiaSharpSvgConverter>().As<ISvgConverter>().InstancePerLifetimeScope();

            // Editor

            builder.RegisterType<AvaloniaImageImporter>().As<IImageImporter>().InstancePerLifetimeScope();
            builder.RegisterType<AvaloniaProjectEditorPlatform>().As<IProjectEditorPlatform>().InstancePerLifetimeScope();
            builder.RegisterType<AvaloniaEditorCanvasPlatform>().As<IEditorCanvasPlatform>().InstancePerLifetimeScope();

            // View

            builder.RegisterType<MainWindow>().As<MainWindow>().InstancePerLifetimeScope();
        }
    }