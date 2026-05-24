#!/bin/bash
eval $(awk 'NR%2==1{name=$0; getline val; print name "=" val}' params.txt)

sed -i \
  -e "s/^height .*/height              ${height};/" \
  -e "s/^channel_bottom_point .*/channel_bottom_point  ${channel_bottom_point};/" \
  -e "s/^channel_top_point .*/channel_top_point     ${channel_top_point};/" \
  -e "s/^triangle_height .*/triangle_height       ${triangle_height};/" \
  -e "s/^inlet_width .*/inlet_width           ${inlet_width};/" \
  -e "s/^left_triangle_point .*/left_triangle_point   ${left_triangle_point};/" \
  -e "s/^channel_length .*/channel_length       ${channel_length};/" \
  -e "s/^half_of_channel_width .*/half_of_channel_width ${half_of_channel_width};/" \
  -e "s/^right_triangle_point .*/right_triangle_point  ${right_triangle_point};/" \
  -e "s/^construction_width .*/construction_width    ${construction_width};/" \
  -e "s/^cell_x1 .*/cell_x1 ${cell_x1};/" \
  -e "s/^cell_x2 .*/cell_x2 ${cell_x2};/" \
  -e "s/^cell_y1 .*/cell_y1 ${cell_y1};/" \
  -e "s/^cell_y2 .*/cell_y2 ${cell_y2};/" \
  -e "s/^cell_y3 .*/cell_y3 ${cell_y3};/" \
  -e "s/^cell_y4 .*/cell_y4 ${cell_y4};/" \
  system/blockMeshDict

sed -i "19s/.*/U ${U};/" 0_org/U

echo params changed
