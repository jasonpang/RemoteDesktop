Remote Desktop, a PC Screen Sharing Software Prototype
========================================================

**Note:** I've been getting some emails about this project (never expected anyone to see this really) and it not working, so I'm going to try to fix it and document it better. Cheers! Hopefully shouldn't take too long. (November 4, 2014)

Tagline
-------
See your friend's screen and control his keyboard and mouse. `RemoteDesktop' brings the remote computer to you, with extra tools like a remote file manager, registry editor, and chat.


Old Website
------------

http://novarat.sourceforge.net/

Screenshots
----------
![](http://novarat.sourceforge.net/screenies/IntroducerMain.png)
![](http://novarat.sourceforge.net/screenies/IntroducerLog.png)
![](http://novarat.sourceforge.net/screenies/Server.png)
![](http://novarat.sourceforge.net/screenies/Client.png)
![](http://novarat.sourceforge.net/screenies/RemoteDesktop.png)

Technical Features & Libraries
----------

* C#
  * [Lidgren UDP networking library](https://code.google.com/p/lidgren-network-gen3/) for basic UDP functionality
  * [Ermau's Tempest networking library](https://github.com/ermau/Tempest) (modified) for a more object-oriented architecture
* [DFMirage mirror driver](http://www.demoforge.com/dfmirage.htm) for extremely fast screen capture

