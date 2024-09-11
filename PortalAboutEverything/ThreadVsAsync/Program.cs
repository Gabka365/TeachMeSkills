using ThreadVsAsync;

//var counter = new Counter();

//counter.Do();//runMethod
//var taskIvan = new Task(() => counter.Count("Ivan"));
//var taskLize = new Task(() => counter.Count("Lize"));

//taskIvan.Start();
//taskLize.Start();

//Console.ReadLine();

var api = new AsyncExampleApi();

//var data1 = await api.CallCatApiAsync();// 4 sec
//var data2 = await api.CallCatApiAsync();// 3 sec
//var data3 = await api.CallCatApiAsync();// 5 sec
//                                        // 12 sec


Task<string> task1 = api.CallCatApiAsync();// 4 sec
Task<string> task2 = api.CallCatApiAsync();// 3 sec
Task<string> task3 = api.CallCatApiAsync();// 5 sec
                                    
Task.WaitAll(task1, task2, task3);          // 5 sec

Console.WriteLine(task1.Result);
Console.WriteLine(task2.Result);
Console.WriteLine(task3.Result);

