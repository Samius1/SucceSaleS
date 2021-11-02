# Task Manager
In this section, we will define the task manager that it's going to be used for our application.<br>
To select it, a research was done online. There were many options, but they were narred down to the next three ones. 

- [Cake](https://cakebuild.net/): Cake is a build automation system with C# DSL. It is free and open source. It allows to compile code, run unit tests, build NuGet packages... It needs Cake Frosting to code script in pure C#. It runs as a console application with IDE integration.
- [psake](https://github.com/psake/psake): psake is a build automation tool coded in PowerShell. It tries to improve the build scripts by using PowerShell syntax. It is inspired in bake and rake. 
- [Nuke](https://nuke.build/): Nuke is a build automation solution with C# DSL. It simplifies the way to manage the project by adding a C# project to the solution, which allows developers to debug the build script or navigate from a target to the actual implementation, for example. It also does package management and even allows the creation of NuGet packages.

After some more digging online, it has been selected Nuke as the most suitable task manager for this project. One of the decisive points is that it allows to use C# to code everything and that it easily integrates third party tools. 