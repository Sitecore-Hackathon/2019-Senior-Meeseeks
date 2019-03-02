# Senior Meeseeks: Machine Learning applied to Xconnect module

## Summary

**Category:** Best use of xConnect and/or Universal Tracker

The purpose of this module is to gather profiling information of the users based on their interactions, this information is displayed on the Product page as a proof of concept. This wa it can help the Content Editor or the Marketer have profiling information, which can later be used to personalize the user experience on the site.

The user is authenticated on the site, then the user will browse through several product pages which have been created for this module. There are around twenty product pages which are Bicycle themed. The user interactions are stored as goals in xDB and retrieved in the system using the xConnect client component. 

Once the user has navegated in some product pages, the system has enough analytic data to send to the machine learning module hosted in Azure to process this data and get the user profiled. The Azure ML takes as input the users interactions and returns a profile of the user based on Bartle's taxonomy of player types. This profiles can be used to personalize the site to enahce the users engagement. 

Bartle taxonomy of player types can profile the user in one of four categories: 

![Image](https://www.google.com/url?sa=i&source=images&cd=&ved=2ahUKEwjI_6-bvOTgAhVxHDQIHQUfB3UQjRx6BAgBEAU&url=https%3A%2F%2Fhypixel.net%2Fthreads%2Fphoenixinfo-player-psychology-and-bartles-taxonomy.1097710%2F&psig=AOvVaw3r84X7bZ0CnUtlDaec2-t_&ust=1551650774179180)

## Pre-requisites

Does your module rely on other Sitecore modules or frameworks?

- List any dependencies
- Or other modules that must be installed
- Or services that must be enabled/configured

## Installation

Provide detailed instructions on how to install the module, and include screenshots where necessary.

1. Use the Sitecore Installation wizard to install the [package](#link-to-package)
2. ???
3. Profit

## Configuration

How do you configure your module once it is installed? Are there items that need to be updated with settings, or maybe config files need to have keys updated?

Remember you are using Markdown, you can provide code samples too:

```xml
<?xml version="1.0"?>
<!--
  Purpose: Configuration settings for my hackathon module
-->
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <settings>
      <setting name="MyModule.Setting" value="Hackathon" />
    </settings>
  </sitecore>
</configuration>
```

## Usage

Provide documentation  about your module, how do the users use your module, where are things located, what do icons mean, are there any secret shortcuts etc.

Please include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://placeimg.com/480/240/any "Random")

## Video

Please provide a video highlighing your Hackathon module submission and provide a link to the video. Either a [direct link](https://www.youtube.com/watch?v=EpNhxW4pNKk) to the video, upload it to this documentation folder or maybe upload it to Youtube...

[![Sitecore Hackathon Video Embedding Alt Text](https://img.youtube.com/vi/EpNhxW4pNKk/0.jpg)](https://www.youtube.com/watch?v=EpNhxW4pNKk)
