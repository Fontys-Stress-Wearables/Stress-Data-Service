using Microsoft.AspNetCore.Mvc;
using StressDataService.Controllers;
using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;

namespace StressDataTests.Controllers;

public class HrvMeasurementControllerTests
{
    private readonly Mock<IHrvMeasurementService> _mockService;
    private readonly Mock<INatsService> _nats;
    
    public HrvMeasurementControllerTests()
    {
        _mockService = new Mock<IHrvMeasurementService>();
        _nats = new Mock<INatsService>();
    }

    [Fact]
    public void GetAll_ReturnsAllHrvMeasurements()
    {
        // Arrange
        _mockService.Setup(service => service.GetAll())
            .ReturnsAsync(GetHrvDtos());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetAll();

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
    }

    [Fact]
    public void GetAll_ReturnsEmpty_WhenMeasurementsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetAll())
            .ReturnsAsync(new List<HrvMeasurementDto>());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetAll();

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }
    
    [Fact]
    public void GetById_ReturnsMeasurement()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

        _mockService.Setup(service => service.GetById(testSessionGuid))
            .ReturnsAsync(GetHrvDto(testSessionGuid));
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetById(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<ActionResult<HrvMeasurementDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var hrvDto = Assert.IsType<HrvMeasurementDto>(okResult.Value);
        Assert.Equal(testSessionGuid, hrvDto.Id);
        Assert.Equal(30, hrvDto.HeartRateVariability);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenMeasurementNotFound()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
        var mockService = new Mock<IHrvMeasurementService>();
        mockService.Setup(service => service.GetById(testSessionGuid))
            .ReturnsAsync((HrvMeasurementDto) null!);
        var controller = new HrvMeasurementsController(mockService.Object, _nats.Object);

        // Act
        var result = controller.GetById(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<ActionResult<HrvMeasurementDto>>>(result);
        var actionResult = task.Result;
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }
    
    [Fact]
    public void GetByPatientId_ReturnsMeasurements()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientId(It.IsAny<Guid>()))
            .ReturnsAsync(GetHrvDtos());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByPatientId(Guid.NewGuid());

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
    }
    
    [Fact]
    public void GetByPatientId_ReturnsEmpty_WhenPatientNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<HrvMeasurementDto>());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByPatientId(new Guid());

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }
    
    [Fact]
    public void GetByPatientIdAndDate_ReturnsMeasurements()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientIdAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .ReturnsAsync(GetHrvDtos());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByPatientIdAndDate(Guid.NewGuid(), DateTime.Now);

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
    }
    
    [Fact]
    public void GetByPatientIdAndDate_ReturnsEmpty_WhenPatientNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientIdAndDate(It.IsAny<Guid>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new List<HrvMeasurementDto>());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByPatientIdAndDate(Guid.NewGuid(), DateTime.Now);

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }
    
    [Fact]
    public void GetByPatientIdAndTimespan_ReturnsMeasurements()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientIdAndTimespan(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(GetHrvDtos());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result =
            controller.GetByPatientIdAndTimespan(Guid.NewGuid(), DateTime.Now, DateTime.Now + TimeSpan.FromHours(1));

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
    }
    
    [Fact]
    public void GetByPatientIdAndTimespan_ReturnsEmpty_WhenPatientNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetByPatientIdAndTimespan(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new List<HrvMeasurementDto>());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByPatientIdAndTimespan(Guid.NewGuid(), DateTime.Now, DateTime.Now + TimeSpan.FromHours(1));

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }
    
    [Fact]
    public void GetByWearableIdAndTimespan_ReturnsMeasurements()
    {
        // Arrange
        _mockService.Setup(service => service.GetByWearableIdAndTimespan(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(GetHrvDtos());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result =
            controller.GetByWearableIdAndTimespan(Guid.NewGuid(), DateTime.Now, DateTime.Now + TimeSpan.FromHours(1));

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
        Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
    }
    
    [Fact]
    public void GetByWearableIdAndTimespan_ReturnsEmpty_WhenWearableNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetByWearableIdAndTimespan(It.IsAny<Guid>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new List<HrvMeasurementDto>());
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);

        // Act
        var result = controller.GetByWearableIdAndTimespan(Guid.NewGuid(), DateTime.Now, DateTime.Now + TimeSpan.FromHours(1));

        // Assert
        var task = Assert.IsType<Task<ActionResult<IEnumerable<HrvMeasurementDto>>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(task.Result.Result);
        var returnValue = Assert.IsType<List<HrvMeasurementDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }
    
    [Fact]
    public void Create_ReturnsBadRequest_GivenInvalidMeasurement()
    {
        // Arrange
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);
        CreateHrvMeasurementDto createHrvMeasurement = new CreateHrvMeasurementDto(new Guid(), new Guid(), DateTime.Now, 10);
        
        // Act
        var result = controller.Create(createHrvMeasurement);

        // Assert
        var task = Assert.IsType<ActionResult<HrvMeasurementDto>>(result);
        var actionResult = task.Result;
        Assert.IsType<BadRequestResult>(actionResult);
    }

    [Fact] 
    public void Create_ReturnsNewlyCreatedMeasurement()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B017D");
        _mockService.Setup(service => service.Create(It.IsAny<CreateHrvMeasurementDto>()))
            .Returns(GetHrvDto(testSessionGuid));
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);
        CreateHrvMeasurementDto hrvDto = new CreateHrvMeasurementDto(
            new Guid(), new Guid(), DateTime.Now, 30);
        
        // Act
        var result = controller.Create(hrvDto);

        // Assert
        var task = Assert.IsType<ActionResult<HrvMeasurementDto>>(result);
        var okResult = Assert.IsType<CreatedAtActionResult>(task.Result);
        var hrv = Assert.IsType<HrvMeasurementDto>(okResult.Value);
        Assert.Equal(testSessionGuid, hrv.Id);
        Assert.Equal(30, hrv.HeartRateVariability);
    }

    [Fact] 
    public void Update_ReturnsNoContent()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B017D");
        UpdateHrvMeasurementDto updateHrvDto = new UpdateHrvMeasurementDto(40);
        var returnedDto = new HrvMeasurement()
        {
            Id =  testSessionGuid,
            PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0111"),
            WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0222"),
            TimeStamp = DateTime.Now,
            HeartRateVariability = 60
        }.AsDto();

        _mockService.Setup(service => service.Update(returnedDto.Id, It.IsAny<UpdateHrvMeasurementDto>()))
            .ReturnsAsync(returnedDto);
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);
        
        
        // Act
        var result = controller.Update(testSessionGuid, updateHrvDto);

        // Assert
        var task = Assert.IsType<Task<ActionResult>>(result);
        Assert.IsType<NoContentResult>(task.Result);
    }
    
    [Fact] 
    public void Update_ReturnsNotFound_WhenMeasurementNotFound()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
        var mockService = new Mock<IHrvMeasurementService>();
        mockService.Setup(service => service.Update(testSessionGuid, It.IsAny<UpdateHrvMeasurementDto>()))
            .ReturnsAsync((HrvMeasurementDto) null!);
        var controller = new HrvMeasurementsController(mockService.Object, _nats.Object);
        UpdateHrvMeasurementDto updateHrvDto = new UpdateHrvMeasurementDto(70);
        
        // Act
        var result = controller.Update(testSessionGuid, updateHrvDto);

        // Assert
        var task = Assert.IsType<Task<ActionResult>>(result);
        Assert.IsType<NotFoundResult>(task.Result);
    }
    
    [Fact] 
    public void Delete_ReturnsNoContent()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B017D");
        _mockService.Setup(service => service.Delete(testSessionGuid))
            .ReturnsAsync(GetHrvDto(testSessionGuid));
        var controller = new HrvMeasurementsController(_mockService.Object, _nats.Object);
        
        // Act
        var result = controller.Delete(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<ActionResult>>(result);
        Assert.IsType<NoContentResult>(task.Result);
    }
    
    [Fact] 
    public void Delete_ReturnsNotFound_WhenMeasurementNotFound()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
        var mockService = new Mock<IHrvMeasurementService>();
        mockService.Setup(service => service.Delete(testSessionGuid))
            .ReturnsAsync((HrvMeasurementDto) null!);
        var controller = new HrvMeasurementsController(mockService.Object, _nats.Object);
        
        // Act
        var result = controller.Delete(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<ActionResult>>(result);
        Assert.IsType<NotFoundResult>(task.Result);
    }
    
    private HrvMeasurementDto GetHrvDto(Guid id)
    {
        return new HrvMeasurement()
        {
            Id = id,
            PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0111"),
            WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0222"),
            TimeStamp = DateTime.Now,
            HeartRateVariability = 30
        }.AsDto();
    }

    private IEnumerable<HrvMeasurementDto> GetHrvDtos()
    {
        List<HrvMeasurementDto> hrvDtos = new List<HrvMeasurementDto>();
        hrvDtos.Add(new HrvMeasurement()
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"),
            PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0456"),
            WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0456"),
            TimeStamp = DateTime.Now,
            HeartRateVariability = 10
        }.AsDto());
        
        hrvDtos.Add(new HrvMeasurement()
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"),
            PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0BBB"),
            WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0CCC"),
            TimeStamp = DateTime.Now,
            HeartRateVariability = 20
        }.AsDto());
        
        return hrvDtos;
    }
}