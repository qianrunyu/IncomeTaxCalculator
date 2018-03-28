# IncomeTaxCalculator
1,What is in it:
->  WPF app with its unit test project.
-> Nuget package to manipulate json format file (Newtonsoft.Json)

2, How to make it work:
-> suggest to run on visual studio 2017 and open the Payment_GaryQian.sln
-> build wpf project and the executable file is "Payment_GaryQian\Payment_GaryQian\bin\Debug\Payment_GaryQian.exe" 
->run it and the UI should look like:

-> A folder called "FilesForImportTesting" contains some csv & json files

3, Let's talk about the UI
-> customized UI appearance for better user experience.
->use file import icon to  pop up the file selection dialog and choose .csv or .json file.
-> click calculate button to see the payment calculation result. Outputs are binding to a datagrid within the UI. You can also click "Save to Drive" and choose a desire output path & name to store the output results.
-> user can use .csv and .json as import format and save the results as either .csv or .json which provides some flexibility. 
->Search textbox filters the "Name" column in datagrid in real time. Please note that the total output rows will not be affected by search function. This only affects the viewing purpose. 
-> Datagrid supports auto sorting based on columns.

4, Let's talk about the source code
-> MVVM pattern with data binding .Minimized code behind is implemented.
-> View is bind with MainWindowViewModel.cs. Employee models are stored in Model folder. Tax calculation rule is written as static class under businessLogic folder. BusinessLogic->ManipulateOutput.cs will convert import object into proper output object.
->The calculation logic is relatively simple. The soul of this solution is error handling. I have found the Gross Income is the key element for the entire calculation life cycle. A wrong Gross Income, such as non-number or negative value will block all calculations. An 'Err.' string is given to the output cell.
The second important value is Super rate %. The desire format is a number with %.  Any unexpected format will result "Err."  in super output. 
 In a nut shell, there are three scenarios: 1, all output value is valid 2, Annual salary in import file is invalid then  Gross income, income tax, net income and Super will all show "Err." in output file. 3, Only Super Rate is invalid and we can still have some useful info in output file. An example shows below: 


-> A wrong value found in a certain row will not force the loop to exit. It will continue calculating till the end of the records. Error message will show on the right spot in output file.  This feature is useful for real world situation. 
-> IO exception while reading/writing file is catched and won't crash the program.
-> Added unit testing in the solution.
  
5, My assumptions to the exercise
-> user has a .csv/json file with correct title and rows to import (first name, last name, annual salary, super rate (%), payment start date)  This will make the job easy.
-> user has a correct title format file but the values in some of the rows are invalid. I try convert the valid value and skip the wrong one.
-> user has an incorrect title format such as first name as FIRST_NAME. For .csv file it is not a bit deal but the json will not accept this as an object key.
-> My output file title format follows (name,pay period,gross income,income tax,net income,super) in both csv and json. 

6, Some other thoughts
-> My solution is in WPF. The other options can be mobile (use .net core), React (with NOTE.js and webpack) or even console.
-> proper interface/abstract class in commercial version solution
-> more file types support such as excel and xml
