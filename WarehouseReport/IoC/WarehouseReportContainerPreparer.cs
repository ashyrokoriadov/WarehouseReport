using System.Collections.Generic;
using Autofac;
using WarehouseReport.Input.Cleaning;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Input.Reading;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Reports;
using WarehouseReport.Repos;

namespace WarehouseReport.IoC
{
    public static class WarehouseReportContainerPreparer
    {
        public static void Prepare(ContainerBuilder containerBuilder, string file)
        {
            RegisterInputMappers(containerBuilder);
            RegisterInputReader(containerBuilder, file);
            RegisterInputCleaner(containerBuilder);
            RegisterRepos(containerBuilder);
            RegisterReportPreparer(containerBuilder);
        }

        private static void RegisterInputMappers(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<WarehouseItemInfoMapper>()
                .As<IMapper<string, WarehouseItemInfo>>()
                .SingleInstance();
            containerBuilder.RegisterType<WarehouseItemInfoListMapper>()
                .As<IMapper<string, IEnumerable<WarehouseItemInfo>>>()
                .SingleInstance();
            containerBuilder.RegisterType<WarehouseItemDtoMapper>()
                .As<IMapper<string, WarehouseItemDto>>()
                .SingleInstance();
        }

        private static void RegisterInputReader(ContainerBuilder containerBuilder, string inputFileName)
        {
            containerBuilder.RegisterType<FileReader>()
                .WithParameter("fileName", inputFileName)
                .As<IInputReader>()
                .SingleInstance();
        }

        private static void RegisterInputCleaner(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DataCleaner>()
                .As<IDataCleaner>()
                .SingleInstance();
        }

        private static void RegisterRepos(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<WarehouseRepo>()
                .As<IRepo<Warehouse, WarehouseItemDto>>()
                .SingleInstance();

            containerBuilder.RegisterType<WarehouseRepoFiller>()
                .As<IRepoFiller<WarehouseItemDto>>()
                .SingleInstance();
        }

        private static void RegisterReportPreparer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<WarehouseReportGenerator>()
                .As<IReportGenerator<IEnumerable<Warehouse>>>();
        }
    }
}
