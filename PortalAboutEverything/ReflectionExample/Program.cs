
using ReflectionExample;
using ReflectionExample.FakeDataLayer;
using System.Reflection;

var userType = typeof(User);

var user = new User();
var theSameType = user.GetType();

Console.WriteLine(userType.Name);

Console.WriteLine("Properties");
var properties = userType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
foreach (var property in properties)
{
    var attributes = property.GetCustomAttributes();
    var attributeString= string.Join(" | ", attributes.Select(x => x.GetType().Name));
    Console.WriteLine($"\t {property.Name} {attributeString}");
}

Console.WriteLine("Fields");
var fields = userType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
foreach (var field in fields)
{
    Console.WriteLine("\t" + field.Name);
}

Console.WriteLine("Methods");
var methods = userType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
foreach (var method in methods)
{
    var isGoodMethod = method.ReturnType == typeof(int);
    Console.WriteLine($"\t {isGoodMethod} {method.Name}");
}

Console.WriteLine("Actions");
var actions = userType
    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
    .Where(m => m.ReturnType == typeof(int));
foreach (var action in actions)
{
    Console.WriteLine($"\t {action.Name}");
}


Console.WriteLine("Constructors");
var constructors = userType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
foreach (var constructor in constructors)
{
    Console.WriteLine("\t" + constructor.GetParameters().Length);
}

var di = new DiPrototype();
var obj = di.CreateObject(typeof(Repository));
Console.WriteLine(obj.GetType());


Console.WriteLine("Age of User getted from private field");
var f = userType.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
var age = f.GetValue(user);
Console.WriteLine($"Age: {age}");

f.SetValue(user, 50);

age = f.GetValue(user);
Console.WriteLine($"Age: {age}");
