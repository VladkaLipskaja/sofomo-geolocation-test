using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using Sofomo.Entities;
using Sofomo.Models;
using Sofomo.Network;
using Sofomo.Services;
using Xunit;

namespace Sofomo.Tests
{
    public class GeolocationServiceTests
    {
        [Theory, AutoMoqData]
        public async Task GetGeolocationAsync_NotExistsIP_GeolocationException(string address, string ip,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>((GeolocationEntity)null));
            
            // Act
            var result = geolocationService.Awaiting(s => s.GetGeolocationAsync(address));

            // Assert
            await result.Should().ThrowAsync<GeolocationException>();
            geolocationHelper.Verify();
            geolocationRepository.Verify();
        }
        
        [Theory, AutoMoqData]
        public async Task GetGeolocationAsync_ExistsIP_Geolocation(string address, string ip,
            GeolocationEntity entity,
            GeolocationDto response,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            [Frozen]Mock<IMapper> mapper,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            mapper.Setup(x => x.Map<GeolocationDto>(entity)).Returns(response);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>(entity));
            
            // Act
            var result = await geolocationService.GetGeolocationAsync(address);

            // Assert
            result.Should().BeEquivalentTo(response);
            geolocationHelper.Verify();
            geolocationRepository.Verify();
            mapper.Verify();
        }
        
        [Theory, AutoMoqData]
        public async Task AddGeolocationAsync_ExistsIP_GeolocationException(string address, string ip,
            GeolocationEntity entity,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>(entity));
            
            // Act
            var result = geolocationService.Awaiting(s => s.AddGeolocationAsync(address));

            // Assert
            await result.Should().ThrowAsync<GeolocationException>();
            geolocationHelper.Verify();
            geolocationRepository.Verify();
        }
        
        [Theory, AutoMoqData]
        public async Task AddGeolocationAsync_NotExistsIP_Geolocation(string address, string ip,
            GeolocationEntity entity,
            GeolocationModel model,
            GeolocationDto response,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            [Frozen]Mock<IMapper> mapper,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            geolocationHelper.Setup(x => x.GetGeolocationAsync(ip)).ReturnsAsync(model);
            mapper.Setup(x => x.Map<GeolocationEntity>(model)).Returns(entity);
            mapper.Setup(x => x.Map<GeolocationDto>(entity)).Returns(response);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>((GeolocationEntity)null));
            geolocationRepository.Setup(x => x.AddAsync(entity))
                .ReturnsAsync(entity);
            
            // Act
            var result = await geolocationService.AddGeolocationAsync(address);

            // Assert
            result.Should().BeEquivalentTo(response);
            geolocationHelper.Verify();
            geolocationRepository.Verify();
            mapper.Verify();
        }
        
        [Theory, AutoMoqData]
        public async Task DeleteGeolocationAsync_NotExistsIP_GeolocationException(string address, string ip,
            GeolocationEntity entity,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>((GeolocationEntity)null));
            
            // Act
            var result = geolocationService.Awaiting(s => s.DeleteGeolocationAsync(address));

            // Assert
            await result.Should().ThrowAsync<GeolocationException>();
            geolocationHelper.Verify();
            geolocationRepository.Verify();
        }
        
        [Theory, AutoMoqData]
        public async Task DeleteGeolocationAsync_ExistsIP_Verified(string address, string ip,
            GeolocationEntity entity,
            GeolocationModel model,
            GeolocationDto response,
            [Frozen]Mock<IGeolocationHelper> geolocationHelper,
            [Frozen]Mock<IRepository<GeolocationEntity>> geolocationRepository,
            GeolocationService geolocationService)
        {
            // Arrange
            geolocationHelper.Setup(x => x.GetIPFromAddressAsync(address)).ReturnsAsync(ip);
            geolocationRepository.Setup(x => x.GetAsync(ip))
                .Returns(new ValueTask<GeolocationEntity>(entity));
            geolocationRepository.Setup(x => x.DeleteAsync(entity))
                .Returns(Task.CompletedTask);
            
            // Act
            await geolocationService.DeleteGeolocationAsync(address);

            // Assert
            geolocationHelper.Verify();
            geolocationRepository.Verify();
        }
    }
}