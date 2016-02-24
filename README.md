# Installation Instructions:

##### Installs Basic MVC structure, and auto builds using project.json with automatic `dnu restore`
##### Run command using your `powershell` (not CMD) within empty Project Folder i.e `$ ~/Desktop/empty_project`
##### Install core files :

```bash
Invoke-WebRequest https://github.com/TimDurward/csharp-task-runner/archive/master.zip -Outfile ~\.\Desktop\c__sharp.zip; Expand-Archive ~\.\Desktop\c__sharp.zip -dest ~\.\Desktop\csharpRunner; mv ~\.\Desktop\csharpRunner\csharp-task-runner-master .; cd .\csharp-task-runner-master\; mv * ..; cd ..; rm .\csharp-task-runner-master\; rm ~\.\Desktop\csharpRunner; rm ~\.\Desktop\c__sharp.zip; dnu restore; clear; echo "Don't forget:"; echo 'git config --global user.name "John Doe"'; echo 'git config --global user.email "johndoe@example.com"'
```

***

<!-- Delete Above -->


<!-- README FILE. Replace where needed -->
# _Application Name_

#### _Short Summary of Application!_

#### By _**Tim Durward**_

## Description

_Brief Description of Application._

## Setup/Installation Requirements

* _Open CMD/Terminal_
* _git clone (github url)_
* _cd into Git Repo_
* `dnx kestrel`
* _open browser (i.e Chrome)_
* navigate to `http://localhost:5004`

## Support and contact details

_Support and Contact Description_

## Technologies Used

* _Main Languages used:_
* _C#, cshtml, Javascript, html, css_
* _Main frameworks used:_
* _.Net, Nancy, Razor, Jquery, bootstrap_

### License

The MIT License (MIT)


Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

**_Tim Durward_** Copyright (c) 2015