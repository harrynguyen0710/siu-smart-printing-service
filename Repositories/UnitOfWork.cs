using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;

namespace siu_smart_printing_service.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IFileTypesRepository _fileTypeRepository;
        private IPrinterRepository _printerRepository;
        private IPrintingLogsRepository _printinglogRepository;
        private IUploadedFileRepository _uploadedFileRepository;
        private IUserRepository _userRepository;
        private IRoomRepository _roomRepository;
        private IConfigurationRepository _configurationRepository;

        public UnitOfWork(ApplicationDbContext context, IFileTypesRepository fileTypeRepository, IPrinterRepository printerRepository,
            IPrintingLogsRepository printinglogRepository, IUploadedFileRepository uploadedFileRepository, IRoomRepository roomRepository,
            IUserRepository userRepository, IConfigurationRepository configurationRepository)
        {
            _context = context;
            _fileTypeRepository = fileTypeRepository;
            _printerRepository = printerRepository;
            _printinglogRepository = printinglogRepository;
            _uploadedFileRepository = uploadedFileRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _configurationRepository = configurationRepository;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<TEntity>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public IRoomRepository RoomRepository
        {
            get
            {
                return _roomRepository ??= new RoomRepository(_context);
            }
        }

        public IConfigurationRepository ConfigurationRepository
        {
            get
            {
                return _configurationRepository ??= new ConfigurationRepository(_context);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }


        public IUploadedFileRepository UploadedFileRepository
        {
            get
            {
                return _uploadedFileRepository ??= new UploadedFileRepository(_context);
            }
        }


        public IPrintingLogsRepository PrintingLogsRepository
        {
            get
            {
                return _printinglogRepository ??= new PrintingLogsRepository(_context);
            }
        }

        public IPrinterRepository PrinterRepository
        {
            get
            {
                return _printerRepository ??= new PrinterRepository(_context);
            }
        }

        public IFileTypesRepository FileTypesRepository
        {
            get
            {
                return _fileTypeRepository ??= new FileTypesRepository(_context);
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
