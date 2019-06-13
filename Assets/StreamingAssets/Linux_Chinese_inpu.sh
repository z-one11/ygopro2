#!/bin/bash
#需要安装 dialog xclip xdotool
#将此脚本设置快捷键即可方便使用
#此脚本仅临时解决Linux无法输入中文问题。Mac尚未找到解决方法。
#虽然ygopro原版也无法输入中文，但是此方法对ygopro原版无效，会敲出乱码。
gdialog --title "YGO Chinese inpu" --inputbox " " 30 80 2>/tmp/ygo_inpu.txt #qt环境的用户请把 gdialog 改为 kdialog
cat /tmp/ygo_inpu.txt | tr -d '\n' | xclip -i -selection clipboard
#xdotool key "Shift+Insert"
xdotool key "Ctrl+v"
