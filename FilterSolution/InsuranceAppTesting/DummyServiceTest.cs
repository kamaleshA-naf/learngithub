using ExceptionFilterAPI.Services;

namespace InsuranceAppTesting
{
    public class Tests
    {
        DummyService _dummyService;
        [SetUp]
        public void Setup()
        {
            _dummyService = new DummyService();
        }

        [Test]
        public void AddPassTest()
        {
            //AAA - Arrange, Act, Assert
            //Arrange
            int a = 5, b = 10;
            //Act
            int result = _dummyService.Add(a, b);
            //Assert
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void GreatestPassTest()
        {
            //Arrange
            int a = 5, b = 10;
            //Act
            int result = _dummyService.FindGreatest(a, b);
            //Assert
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void GreatestExceptionTest()
        {
            //Arrange
            int a = 5, b = 5;
            //Act and Assert
            Assert.Throws<InvalidOperationException>(() => _dummyService.FindGreatest(a, b));
        }

        [TearDown]
        public void TearDown()
        {
            _dummyService = null;
        }

    }
}