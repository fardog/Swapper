#!/bin/sh

convert TrayIconLightLeft.png -define icon:auto-resize=16,32,48,128,256 -compress zip TrayIconLightLeft.ico
convert TrayIconLightRight.png -define icon:auto-resize=16,32,48,128,256 -compress zip TrayIconLightRight.ico
convert TrayIconDarkLeft.png -define icon:auto-resize=16,32,48,128,256 -compress zip TrayIconDarkLeft.ico
convert TrayIconDarkRight.png -define icon:auto-resize=16,32,48,128,256 -compress zip TrayIconDarkRight.ico
