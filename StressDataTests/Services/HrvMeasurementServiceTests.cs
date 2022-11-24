using StressDataService.Dtos;
using StressDataService.Interfaces;
using StressDataService.Models;
using StressDataService.Services;

namespace StressDataTests.Services;

public class HrvMeasurementServiceTests
{
    private readonly Mock<IHrvMeasurementRepository> _mockRepository;
    
    public HrvMeasurementServiceTests()
    {
        _mockRepository = new Mock<IHrvMeasurementRepository>();
    }
    
   [Fact]
   public void GetAll_ReturnsHrvMeasurements()
   {
       // Arrange
       _mockRepository.Setup(repo => repo.GetAll())
           .ReturnsAsync(GetHrvDtos());
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetAll();

       // Assert
       var task = Assert.IsType<Task<IEnumerable<HrvMeasurementDto>>>(result);
       var returnValue = task.Result.ToList();
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
   }

   [Fact]
   public void GetById_ReturnsMeasurement()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
           .ReturnsAsync(GetHrvDto(testSessionGuid));
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetById(testSessionGuid);

       // Assert
       var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
       var hrvDto = Assert.IsType<HrvMeasurementDto>(task.Result);
       Assert.Equal(testSessionGuid, hrvDto.Id);
       Assert.Equal(30, hrvDto.HeartRateVariability);
   }
   
   [Fact]
   public void GetById_ReturnsNull_WhenMeasurementNotFound()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");

       _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
           .ReturnsAsync((HrvMeasurementDto) null!);
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetById(testSessionGuid);

       // Assert
       var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
       Assert.Null(task.Result);
   }

   [Fact]
   public void GetByPatientId_ReturnsHrvMeasurements()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetByPatientId(testSessionGuid))
           .ReturnsAsync(GetHrvDtos());
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetByPatientId(testSessionGuid);

       // Assert
       var task = Assert.IsType<Task<IEnumerable<HrvMeasurementDto>>>(result);
       var returnValue = task.Result.ToList();
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
   }
   
   [Fact]
   public void GetByPatientIdAndDate_ReturnsHrvMeasurements()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetByPatientIdAndDate(testSessionGuid, It.IsAny<DateTime>()))
           .ReturnsAsync(GetHrvDtos());
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetByPatientIdAndDate(testSessionGuid, DateTime.Now);

       // Assert
       var task = Assert.IsType<Task<IEnumerable<HrvMeasurementDto>>>(result);
       var returnValue = task.Result.ToList();
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
   }
   
   [Fact]
   public void GetByPatientIdAndTimespan_ReturnsHrvMeasurements()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetByPatientIdAndTimespan(
               testSessionGuid, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
           .ReturnsAsync(GetHrvDtos());
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetByPatientIdAndTimespan(
           testSessionGuid, DateTime.Now, DateTime.Now + TimeSpan.FromDays(1));

       // Assert
       var task = Assert.IsType<Task<IEnumerable<HrvMeasurementDto>>>(result);
       var returnValue = task.Result.ToList();
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
   }

   [Fact]
   public void GetByWearableIdAndTimespan_ReturnsHrvMeasurements()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetByWearableIdAndTimespan(
               testSessionGuid, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
           .ReturnsAsync(GetHrvDtos());
       var service = new HrvMeasurementService(_mockRepository.Object);

       // Act
       var result = service.GetByWearableIdAndTimespan(
           testSessionGuid, DateTime.Now, DateTime.Now + TimeSpan.FromDays(1));

       // Assert
       var task = Assert.IsType<Task<IEnumerable<HrvMeasurementDto>>>(result);
       var returnValue = task.Result.ToList();
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"), returnValue[0].Id);
       Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"), returnValue[1].Id);
   }
   
   [Fact]
   public void Create_ReturnsMeasurement()
   {
       // Arrange
       var service = new HrvMeasurementService(_mockRepository.Object);
       CreateHrvMeasurementDto hrvDto = new CreateHrvMeasurementDto(
           new Guid("123A647C-AD54-4BCC-A860-E5A2664B019D"), 
           new Guid("456A647C-AD54-4BCC-A860-E5A2664B017D"),
           DateTime.Now, 
           50
       );
       
       // Act
       var result = service.Create(hrvDto);

       // Assert
       var dto = Assert.IsType<HrvMeasurementDto>(result);
       Assert.Equal(new Guid("123A647C-AD54-4BCC-A860-E5A2664B019D"), dto.PatientId);
       Assert.Equal(50, dto.HeartRateVariability);
   }
   
   [Fact]
   public void Update_ReturnsUpdatedMeasurement()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B017D");
       _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
           .ReturnsAsync(GetHrvDto(testSessionGuid));
       var service = new HrvMeasurementService(_mockRepository.Object);
       UpdateHrvMeasurementDto updateHrvMeasurementDto = new UpdateHrvMeasurementDto(35);
       
       // Act
       var result = service.Update(testSessionGuid, updateHrvMeasurementDto);

       // Assert
       var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
       var updatedMeasurement = task.Result;
       Assert.Equal(35, updatedMeasurement.HeartRateVariability);
   }

   [Fact]
   public void Update_ReturnsNull_WhenMeasurementNotFound()
   {
       // Arrange
       Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
       _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
           .ReturnsAsync((HrvMeasurementDto) null!);
       var service = new HrvMeasurementService(_mockRepository.Object);
       UpdateHrvMeasurementDto updateHrvDto = new UpdateHrvMeasurementDto(35);

       // Act
       var result = service.Update(testSessionGuid, updateHrvDto);

       // Assert
       var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
       Assert.Null(task.Result);
   }
   
    [Fact] 
    public void Delete_ReturnsDeletedMeasurement()
    {
        // Arrange
        Guid testSessionGuid = new Guid("222A647C-AD54-4BCC-A860-E5A2664B017D");
        _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
            .ReturnsAsync(GetHrvDto(testSessionGuid));
        var service = new HrvMeasurementService(_mockRepository.Object);
        
        // Act
        var result = service.Delete(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
        var deletedMeasurement = task.Result;
        Assert.Equal(testSessionGuid, deletedMeasurement.Id);
    }

    [Fact] 
    public void Delete_ReturnsNull_WhenMeasurementNotFound()
    {
        // Arrange
        Guid testSessionGuid = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B017D");
        _mockRepository.Setup(repo => repo.GetById(testSessionGuid))
            .ReturnsAsync((HrvMeasurementDto) null!);
        var service = new HrvMeasurementService(_mockRepository.Object);
        
        // Act
        var result = service.Delete(testSessionGuid);

        // Assert
        var task = Assert.IsType<Task<HrvMeasurementDto>>(result);
        Assert.Null(task.Result);
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
       List<HrvMeasurementDto> hrvDtos = new List<HrvMeasurementDto>
       {
           new HrvMeasurement()
           {
               Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0123"),
               PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0456"),
               WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0456"),
               TimeStamp = DateTime.Now,
               HeartRateVariability = 10
           }.AsDto(),
           new HrvMeasurement()
           {
               Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0AAA"),
               PatientId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0BBB"),
               WearableId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0CCC"),
               TimeStamp = DateTime.Now,
               HeartRateVariability = 20
           }.AsDto()
       };

       return hrvDtos;
   }
}