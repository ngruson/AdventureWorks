﻿using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
using AW.Services.Product.Core.Handlers.StoreProductPhotos;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using MediatR;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class StoreProductPhotosCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_FilesDoNotExists_WriteFiles(
            [Frozen] Mock<IMediator> mediatorMock,
            [Frozen] Mock<IFileHandler> fileWriterMock,
            StoreProductPhotosCommandHandler sut,
            StoreProductPhotosCommand command,
            List<ProductWithPhotoDto> products
        )
        {
            // Arrange
            mediatorMock.Setup(_ => _.Send(
                    It.IsAny<GetAllProductsWithPhotosQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(products);

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            fileWriterMock.Verify(_ => _.WriteFile(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>()
                ),
                Times.Exactly(products.Count * 3 * 2)
            // 3 products * 3 photos * 2 (thumbnail + large photo)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WriteFilesFails_NoFilesWritten(
            [Frozen] Mock<IMediator> mediatorMock,
            [Frozen] Mock<IFileHandler> fileWriterMock,
            StoreProductPhotosCommandHandler sut,
            StoreProductPhotosCommand command,
            List<ProductWithPhotoDto> products
        )
        {
            // Arrange
            mediatorMock.Setup(_ => _.Send(
                    It.IsAny<GetAllProductsWithPhotosQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(products);

            fileWriterMock.Setup(_ => _.WriteFile(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>()
                )
            )
            .Throws<IOException>();

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            fileWriterMock.Verify(_ => _.WriteFile(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>()
                ),
                Times.Exactly(products.Count * 3 * 2)
            // 3 products * 3 photos * 2 (thumbnail + large photo)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_FilesExist_WriteNoFiles(
            [Frozen] Mock<IMediator> mediatorMock,
            [Frozen] Mock<IFileHandler> fileWriterMock,
            StoreProductPhotosCommandHandler sut,
            StoreProductPhotosCommand command,
            List<ProductWithPhotoDto> products
        )
        {
            // Arrange
            mediatorMock.Setup(_ => _.Send(
                    It.IsAny<GetAllProductsWithPhotosQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(products);

            fileWriterMock.Setup(_ => _.FileExists(It.IsAny<string>()))
                .Returns(true);

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            fileWriterMock.Verify(_ => _.WriteFile(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>()
                ),
                Times.Never
            );
        }
    }
}
