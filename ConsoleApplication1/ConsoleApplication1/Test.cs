using Moq;

// Assumptions:
public class Bar
{
    // Bar implementation
}

public interface IFoo
{
    bool DoSomething(string);
    string DoSomethingStringy(string);
    bool TryParse(string, out string);
    bool Submit(ref Bar);
    int GetCount();
    int GetCountThing();
}

var mock = new Mock<IFoo>();
mock.Setup(foo => foo.DoSomething("ping")).Returns(true);


// out arguments
var outString = "ack";
// TryParse will return true, and the out argument will return "ack", lazy evaluated
mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);


// ref arguments
var instance = new Bar();
// Only matches if the ref argument to the invocation is the same instance
mock.Setup(foo => foo.Submit(ref instance)).Returns(true);


// access invocation arguments when returning a value
mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
		.Returns((string s) => s.ToLower());
// Multiple parameters overloads available


// throwing when invoked with specific parameters
mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));


// lazy evaluating return value
mock.Setup(foo => foo.GetCount()).Returns(() => count);


// returning different values on each invocation
var mock = new Mock<IFoo>();
var calls = 0;
mock.Setup(foo => foo.GetCountThing())
	.Returns(() => calls)
	.Callback(() => calls++);
// returns 0 on first invocation, 1 on the next, and so on
Console.WriteLine(mock.Object.GetCountThing());
