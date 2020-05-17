using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using WarehouseReport.Input.Cleaning;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Input.Reading;
using WarehouseReport.IoC;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Reports;
using WarehouseReport.Repos;

namespace WarehouseReport
{
    class Program
    {
        private static IContainer _container;
        private static IInputReader _reader;
        private static IDataCleaner _cleaner;
        private static IMapper<string, WarehouseItemDto> _warehouseItemDtoMapper;
        private static IRepo<Warehouse, WarehouseItemDto> _warehouseRepo;
        private static IRepoFiller<WarehouseItemDto> _warehouseFillerRepo;
        private static IReportGenerator<IEnumerable<Warehouse>> _reportGenerator;

        private static void Main(string[] args)
        {
            DisplayInfoIfNeeded(args);
            PrepareContainer(args[0]);
            ResolveDependencies();
            RunBusinessLogic();

            Console.ReadKey();
        }

        private static void DisplayInfoIfNeeded(string[] args)
        {
            if (args.Length != 0 && args.Length == 1) return;

            Console.WriteLine($"Invalid number of arguments: {args.Length}.");
            Console.WriteLine("Application accepts following arguments:");
            Console.WriteLine(" <path_to_file> - for an input as a TXT file; example: WarehouseReport.exe sampleData.txt");
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit application...");
            Console.ReadKey();

            Environment.Exit(0);
        }

        private static void PrepareContainer(string file)
        {
            var builder = ContainerPreparer.Builder;
            WarehouseReportContainerPreparer.Prepare(builder, file);
            _container = ContainerPreparer.Container;
        }

        private static void ResolveDependencies()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                _reader = scope.Resolve<IInputReader>();
                _cleaner = scope.Resolve<IDataCleaner>();
                _warehouseItemDtoMapper = scope.Resolve<IMapper<string, WarehouseItemDto>>();
                _warehouseRepo = scope.Resolve<IRepo<Warehouse, WarehouseItemDto>>();
                _warehouseFillerRepo = scope.Resolve<IRepoFiller<WarehouseItemDto>>();
                _reportGenerator = scope.Resolve<IReportGenerator<IEnumerable<Warehouse>>>();
            }
        }

        private static void RunBusinessLogic()
        {
            var data = _reader.Read();
            var clearData = _cleaner.Clean(data);
            var warehouseItems = clearData.Select(datum => _warehouseItemDtoMapper.Map(datum)).ToList();
            _warehouseFillerRepo.Fill(warehouseItems);
            var report = _reportGenerator.GenerateReport(_warehouseRepo.GetAll());
            Console.WriteLine(report);
        }
    }
}
