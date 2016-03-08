LORA
=======================

Introduction
------------
This is a simple, skeleton application using the ZF2 MVC layer and module
systems. This application is meant to be used as a starting place for those
looking to get their feet wet with ZF2, Firebase and NoSQL.

Application is called LORA - because of LyuboOrlinRobotAcronym. The idea behind this project is to demonstrate
how easy is to send data from a robot to NoSQL database in the cloud and the show the information.



### What we are using

`Firebase` - NoSQL solution in the cloud (Google based service) - https://www.firebase.com/
`Plotly.js` - using Plotly - https://plot.ly/javascript/

### HowToBasic

We are using `Firebase` for our cloud NoSQL storage. It is new, quick and pretty easy to use solution. In order to get it
working for you, you will have to make a registration in there. You could check `howTo1.png' located in `docs` folder.

You could check how data structure looks like in `docs` folder - `loraFirebaseStructure.png`
You could check how data is displayed by accessing - your-virtual-holder.localhost/preview (You must have data in your db to see the graphic)
Sample graphic data located in `docs` folder - `dummyDataPreview.png`

Main idea behind LORA was to show how easy is to set up this. There are no security checks(We have left them for you)

Files that we have changed are:
`Karelv1_Robot/LORA/module/Application/config/module.config.php`
`Karelv1_Robot/LORA/module/Application/src/Application/Controller/IndexController.php`
`Karelv1_Robot/LORA/module/Application/view/layout/layout.phtml`
`Karelv1_Robot/LORA/module/Application/view/application/index/preview.phtml` - this file was created