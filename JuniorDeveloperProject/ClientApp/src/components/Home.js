import React, { Component } from 'react';
import designImage from './designDiagram.png';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
            <h1>Demo web application!</h1>
            <div class="row">

                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Task.</h2>
                            <p class="card-text">Build an application which calls <a href="https://dwp-techtest.herokuapp.com" rel="external nofollow" target="blank">this API</a>, and returns people who are listed as either living in London, or whose current coordinates are within 50 miles of London.</p>
                        </div>
                    </div>
                    <br />
                </div>

                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Answer.</h2>
                            <p class="card-text">Table with results is presented in separate view.</p>
                            <a class="btn btn-primary" href="/fetch-data">Fetch data</a>
                        </div>
                    </div>
                    <br />
                </div>

                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Code.</h2>
                            <p class="card-text">Code for this solution can be found in <a href="https://github.com/Developer-RN/JuniorDeveloperProject" rel="external nofollow" target="blank">this Github repo</a>.</p>
                        </div>
                    </div>
                    <br />
                </div>

                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Approach.</h2>
                            <p class="card-text">This was my first attempt to build  an application which calls <a href="https://dwp-techtest.herokuapp.com" rel="external nofollow" target="blank">this API</a>, and returns people who are listed as either living in London, or whose current coordinates are within 50 miles of London. I used AWS and deployed the application using beanstalk. For the code I used .Net core and react js template. The code is written in c#, javacript and Bootstrap to make the application mobile responsive. I am also learning serveless application.
                            </p>
                            <div>

                            </div>
                        </div>
                    </div>
                    <br />
                </div>

                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Used technologies and frameworks.</h2>
                            <ul>
                                <li class="list"><a href="https://reactjs.org/" rel="external nofollow" target="blank">ReactJS</a> client side web application (SPA)</li>
                                <li class="list"><a href="https://dotnet.microsoft.com/en-us/" rel="external nofollow" target="blank">.NET 6.0</a> server side code</li>
                                <li class="list"><a href="https://getbootstrap.com/" rel="external nofollow" target="blank">Bootstrap</a> client side web application styling</li>
                                <li class="list"><a href="https://nodejs.org/en/" rel="external nofollow" target="blank">Node.js & Npm</a> runtime and package manager</li>
                                <li class="list"><a href="https://visualstudio.microsoft.com/vs/" rel="external nofollow" target="blank">Microsoft Visual Studio 2022</a> integrated development environment (IDE)</li>
                                <li class="list"><a href="https://aws.amazon.com/visualstudio/" rel="external nofollow" target="blank">AWS Toolkit for Visual Studio</a> IDE plug-in</li>
                                <li class="list"><a href="https://aws.amazon.com/elasticbeanstalk/" rel="external nofollow" target="blank">AWS Elastic Beanstalk</a> easy-to-use platform as service (PaaS) for deploying and scaling web applications</li>
                                <li class="list"><a href="https://github.com/" rel="external nofollow" target="blank">GitHub</a> source control management (GIT)</li>
                            </ul>
                        </div>
                    </div>
                    <br />
                </div>

            </div>



        </div>
    );
  }
}
