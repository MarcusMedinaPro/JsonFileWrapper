using System.IO;
using JsonFileWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonFileWrapperTests
{
    [TestClass()]
    public class JsonFileTests
    {
        /// <summary>
        /// The filename.
        /// </summary>
        private const string filename = "Hello.txt";

        /// <summary>
        /// Cleans up the tests.
        /// </summary>
        [TestCleanup]
        public void CleanUp ()
        {
            if (File.Exists(filename))
                File.Delete(filename);
        }

        /// <summary>
        /// Initialize the tests
        /// </summary>
        [TestInitialize]
        public void Init ()
        {
            if (File.Exists(filename))
                File.Delete(filename);
        }

        /// <summary>
        /// Jsons the file test.
        /// </summary>
        [TestMethod()]
        public void JsonFileTest ()
        {
            var file = new JsonFile<string>("");
            Assert.IsNotNull(file);
        }

        /// <summary>
        /// Loads a test.
        /// </summary>
        [TestMethod()]
        public void LoadTest ()
        {
            // Arrange
            var expected = "Hello Crazy World";
            File.WriteAllText(filename, expected);
            var file = new JsonFile<string>(filename);

            // Act
            file.Load();

            // Assert
            Assert.AreEqual(expected, file.Data);
        }

        /// <summary>
        /// Saves a test.
        /// </summary>
        [TestMethod()]
        public void SaveTest ()
        {
            //Arrange
            var file = new JsonFile<string>(filename);
            file.Data = "Hello Crazy World";

            // Act
            file.Save();

            // Assert
            Assert.IsNotNull(file);
        }
    }
}