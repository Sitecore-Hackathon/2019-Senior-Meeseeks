# Senior Meeseeks: Machine Learning applied to Xconnect module

## Summary

**Category:** Best use of xConnect and/or Universal Tracker

The purpose of this module is to gather profiling information of the users based on their interactions, this information is displayed on the Product page as a proof of concept. This way it can help the Content Editor or the Marketer have profiling information, which can later be used to personalize the user experience on the site.

The user is authenticated on the site, then the user will browse through several product pages which have been created for this module. There are around twenty product pages which are Bicycle themed. The user interactions are stored as goals in xDB and retrieved in the system using the xConnect client component. 

Once the user has navigated in some product pages, the system has enough analytic data to send to the machine learning module hosted in Azure to process this data and get the user profiled. The Azure ML takes as input the users interactions and returns a profile of the user based on Bartle's taxonomy of player types. This profiles can be used to personalize the site to enhance the user's engagement. 

Bartle taxonomy of player types can profile the user in one of four categories: 

![Image](https://github.com/Sitecore-Hackathon/2019-Senior-Meeseeks/blob/master/documentation/images/Capture2.JPG)

Finally a radial diagram will be displaed on the Product page to show the predicted profile retrieved from the Azure ML service.

![Image](https://github.com/Sitecore-Hackathon/2019-Senior-Meeseeks/blob/master/documentation/images/Capture3.JPG)

## Pre-requisites

This module has the following dependencies:

- Sitecore Habitat running on the Sitecore xperience  Initial release.
- Sitecore License

## Installation


1. Use the Sitecore Installation wizard to install the [packages](https://github.com/Sitecore-Hackathon/2019-Senior-Meeseeks/tree/master/sc.package) contained in the link.


## Configuration

No configuration is needed.

## Usage

The user must login to the Sitecore habitat site. Once logged click on the products menu option located on the header.

After this a list of bike products will appear.

The intention is that the user simulates browsing though these products in order to xConnect to capture more information:

![Image](https://github.com/Sitecore-Hackathon/2019-Senior-Meeseeks/blob/master/documentation/images/Capture.JPG)

Once there are 

## Video

Please provide a video highlighing your Hackathon module submission and provide a link to the video. Either a [direct link](https://www.youtube.com/watch?v=EpNhxW4pNKk) to the video, upload it to this documentation folder or maybe upload it to Youtube...

[![Sitecore Hackathon Video Embedding Alt Text](https://img.youtube.com/vi/EpNhxW4pNKk/0.jpg)](https://www.youtube.com/watch?v=EpNhxW4pNKk)
