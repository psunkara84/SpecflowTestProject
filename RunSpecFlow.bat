:: run tests, will generate XML test results
"packages\NUnit.Runners.2.6.4\tools\nunit-console.exe" GoogleSignin\bin\Debug\GoogleSignin.dll /framework=net-4.0

:: generate HTML test report based on the XML test results
"packages\allure-commandline\bin\allure" generate -o AllureReports -- AllureResults
