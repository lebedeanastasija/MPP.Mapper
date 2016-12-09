using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassMapper;
using ClassMapperTest.TestClasses;

namespace ClassMapperTest
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void Map_WhenSuitablePropertiesTypes_ShouldMapAllProperties()
        {
            Mapper mapper = new Mapper();

            SmallElephant elephant1 = new SmallElephant()
            {
                Weight = 1,
                Age = 10,
                Name = "SmallElephant"
            };

            BigElephant elephant2 = mapper.Map<SmallElephant, BigElephant>(elephant1);

            Assert.IsTrue(elephant2.Weight == 1);
            Assert.IsTrue(elephant2.Age == 10);
            Assert.IsTrue(elephant2.Name == "SmallElephant");
            Assert.IsNull(elephant2.PassportId);
        }

        [TestMethod]
        public void Map_WhenAllUnsuitablePropertiesTypes_ShouldNotMapAnyProperty()
        {
            Mapper mapper = new Mapper();

            IncorrectSmallElephant elephant1 = new IncorrectSmallElephant()
            {
                Weight = 1,
                Age = 0.5f,
                Name = 0
            };

            BigElephant elephant2 = mapper.Map<IncorrectSmallElephant, BigElephant>(elephant1);

            Assert.IsTrue(elephant2.Weight == 0);
            Assert.IsTrue(elephant2.Age == 0);
            Assert.IsNull(elephant2.Name);
            Assert.IsNull(elephant2.PassportId);
        }

        [TestMethod]
        public void Map_WhenMapObjectWithoutProperties_ShouldNotMapAnyProperty()
        {
            Mapper mapper = new Mapper();

            EmptyElephant elephant1 = new EmptyElephant()
            {
            };

            BigElephant elephant2 = mapper.Map<EmptyElephant, BigElephant>(elephant1);

            Assert.IsTrue(elephant2.Weight == 0);
            Assert.IsTrue(elephant2.Age == 0);
            Assert.IsNull(elephant2.Name);
            Assert.IsNull(elephant2.PassportId);
        }

        [TestMethod]
        public void Map_WhenMapBiggerTypePropertyToSmallerOne_ShouldNotMapProperty()
        {
            Mapper mapper = new Mapper();

            BigElephant elephant1 = new BigElephant()
            {
                Weight = 100,
                Age = 100,
                Name = "BigElephant"
            };

            SmallElephant elephant2 = mapper.Map<BigElephant, SmallElephant>(elephant1);

            Assert.IsTrue(elephant2.Weight == 0);
            Assert.IsTrue(elephant2.Age == 0);
            Assert.IsTrue(elephant2.Name == "BigElephant");
        }

        [TestMethod]
        public void Map_WhenSuitablePropertyTypeButDifferentPropertyName_ShouldNotMapProperty()
        {
            Mapper mapper = new Mapper();

            PseudoSmallElephant elephant1 = new PseudoSmallElephant()
            {
                PseudoWeight = 5,
                PseudoAge = 10,
                PseudoName = "PseudoSmallElephant"
            };

            BigElephant elephant2 = mapper.Map<PseudoSmallElephant, BigElephant>(elephant1);

            Assert.IsTrue(elephant2.Weight == 0);
            Assert.IsTrue(elephant2.Age == 0);
            Assert.IsNull(elephant2.Name);
            Assert.IsNull(elephant2.PassportId);
        }

    }
}
