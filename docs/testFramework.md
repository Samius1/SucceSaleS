# Test Framework and Assert library
In this section, we will define the test framework and assert library for our project. 
## Test Framework
Related to the test framework, after some digging thre are three main candidates, which are the ones shown below. 
- [MSTest V2](https://github.com/microsoft/testfx): MSTest V2 is a open source improved version of MSTest framework, the default one included in Visual Studio. It can target .NET Framework, .NET Core and ASP.NET Core.
- [NUnit](https://nunit.org/): NUnit is a test framework ported from JUnit. It is open source
- [XUnit](): XUnit was built from scratch based on NUnit as a new test framework. It improves the localization of the test

XUnit has been selected as the test framework to be used, since it improves some features of NUnit and MSTest, like test isolation. The next table is an extract from [this article](https://www.lambdatest.com/blog/nunit-vs-xunit-vs-mstest/), where some differences over the test frameworks are shown.

| Description                                                                                      | NUnit                 | MSTest               | xUnit                  |
|--------------------------------------------------------------------------------------------------|-----------------------|----------------------|------------------------|
| Marks a test method/individual test                                                              | [Test]                | [TestMethod]         | [Fact]                 |
| Indicates that a class has a group of unit tests                                                 | [TestFixture]         | [TestClass]          | N.A                    |
| Contains the initialization code, which is triggered before every test case                      | [SetUp]               | [TestInitialize]     | Constructor            |
| Contains the cleanup code, which is triggered after every test case                              | [TearDown]            | [TestCleanup]        | IDisposable.Dispose    |
| Contains method that is triggered once before test cases start                                   | [OneTimeSetUp]        | [ClassInitialize]    | IClassFixture<T>       |
| Contains method that is triggered once before test cases end                                     | [OneTimeTearDown]     | [ClassCleanup]       | IClassFixture<T>       |
| Contains per-collection fixture setup and teardown                                               | N.A                   | N.A                  | ICollectionFixture<T>  |
| Ignores a test case                                                                              | [Ignore(“reason”)]    | [Ignore]             | [Fact(Skip=”reason”)]  |
| Categorize test cases or classes                                                                 | [Category()]          | [TestCategory(“)]    | [Trait(“Category”, “”) |
| Identifies a method that needs to be called before executing any test in test class/test fixture | [TestFixtureSetup]    | [ClassInitialize]    | N.A                    |
| Identifies a method that needs to be called after executing any test in test class/test fixture  | [TestFixtureTearDown] | [ClassCleanUp]       | N.A                    |
| Identifies a method that needs to be called before the execution of any tests in Test Assembly   | N.A                   | [AssemblyInitialize] | N.A                    |
| Identifies a method that needs to be called after execution of tests in Test Assembly            | N.A                   | [AssemblyCleanUp]    | N.A                    |

## Assert Library
In this case, it has been difficult to decide. By default, Microsoft has an assert library integrated in the framework that works perfectly. To select an assert library, the first step taken has been to decide if I was to do TDD/BDD or not. It is obvious that the software development technique must be one of them, TDD or BDD. So the default assert library can not be chosen. Right below there are two libraries that facilitates test development.
- <strong>[Fluent Assertion](https://fluentassertions.com/)</strong>. Fluent Assertions allows to define readable TDD or BDD tests. The expected outcome of the test are more human readable assert than normal assert libraries. Instead of the usual Assert.AreEqual() or Assert.IsTrue() with some lambda expressions inside, it has a human syntax like myString.Should().StartWith(). Due to this new approach to testing, the error messages are also more readable.
- <strong>[NFluent](http://www.n-fluent.net/)</strong>. This library is similar to the previous one. It allows to write understandable asserts and throws pretty error messages. It is inspired in Jave FEST Fluent library. Comparing both of them over the examples found, it seems that error messages are more understandable in this library than the previous one.

Both libraries are pretty similar, but due to the selected .NET framework (5.0), NFluent has been finally chosen as the assert library. Fluent Assertion states the frameworks where it can be used in its main page, and .NET Framework 5.0 is none of them.
