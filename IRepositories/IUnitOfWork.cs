namespace siu_smart_printing_service.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        IFileTypesRepository FileTypesRepository { get; }
        IPrinterRepository PrinterRepository { get; }
        IPrintingLogsRepository PrintingLogsRepository { get; }
        IUploadedFileRepository UploadedFileRepository { get; }
        IRoomRepository RoomRepository { get; }
        IUserRepository UserRepository { get; }
        IConfigurationRepository ConfigurationRepository { get; }
        Task<int> CompleteAsync();
    }
}
