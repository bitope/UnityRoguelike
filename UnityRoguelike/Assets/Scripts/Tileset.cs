using System.Collections.Generic;
using System.Resources;

namespace Dungeon
{
    public static class Tileset
    {
        //Floors
        public static int[] f_unseen = new[] { 0 };
        public static int[] f_error = new[] { 1 };
        public static int[] f_grey_dirt = new[] { 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int[] f_grey_dirt_b = new[] { 10, 11, 12, 13, 14, 15, 16, 17 };
        public static int[] f_pebble = new[] {18, 19, 20, 21, 22, 23, 24, 25, 26};
        public static int[] f_pebble_brown = new[] { 27, 28, 29, 30, 31, 32, 33, 34, 35 };
        public static int[] f_pebble_blue = new[] { 36, 37, 38, 39, 40, 41, 42, 43, 44 };
        public static int[] f_pebble_green = new[] { 45, 46, 47, 48, 49, 50, 51, 52, 53 };
        public static int[] f_pebble_cyan = new[] { 54, 55, 56, 57, 58, 59, 60, 61, 62 };
        public static int[] f_pebble_red = new[] { 63, 64, 65, 66, 67, 68, 69, 70, 71 };
        public static int[] f_pebble_magenta = new[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 };
        public static int[] f_pebble_darkgrey = new[] { 81, 82, 83, 84, 85, 86, 87, 88, 89 };
        public static int[] f_pebble_lightblue = new[] { 90, 91, 92, 93, 94, 95, 96, 97, 98 };
        public static int[] f_pebble_lightgreen = new[] { 99, 100, 101, 102, 103, 104, 105, 106, 107 };
        public static int[] f_pebble_lightcyan = new[] { 108, 109, 110, 111, 112, 113, 114, 115, 116 };
        public static int[] f_pebble_lightred = new[] { 117, 118, 119, 120, 121, 122, 123, 124, 125 };
        public static int[] f_pebble_lightmagenta = new[] { 126, 127, 128, 129, 130, 131, 132, 133, 134 };
        public static int[] f_pebble_yellow = new[] { 135, 136, 137, 138, 139, 140, 141, 142, 143 };
        public static int[] f_pebble_white = new[] { 144, 145, 146, 147, 148, 149, 150, 151, 152 };
        public static int[] f_cave = new[] { 153, 154, 155, 156, 157, 158, 159, 160, 161 };
        public static int[] f_mesh = new[] { 162, 163, 164, 165 };
        public static int[] f_mud = new[] { 166, 167, 168, 169 };
        public static int[] f_ice = new[] { 170, 171, 172, 173 };
        public static int[] f_lair = new[] { 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189 };
        public static int[] f_orc = new[] { 190, 191, 192, 193, 194, 195, 196, 197 };
        public static int[] f_moss = new[] { 198, 199, 200, 201 };
        public static int[] f_bog_green = new[] { 202, 203, 204, 205 };
        public static int[] f_acidic_floor = new[] { 206, 207, 208, 209 };
        public static int[] f_slime_overlay_BROKEN = new[] { 210, 211, 212 };
        public static int[] f_snake_a = new[] { 213, 214, 215, 216 };
        public static int[] f_snake_c = new[] { 217, 218, 219, 220 };
        public static int[] f_snake_d = new[] { 221, 222, 223, 224 };
        public static int[] f_swamp = new[] { 225, 226, 227, 228 };
        public static int[] f_spider = new[] { 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240 };
        public static int[] f_tomb = new[] { 241, 242, 243, 244 };
        public static int[] f_rect_gray = new[] { 245, 246, 247, 248 };
        public static int[] f_floor_vines = new[] { 249, 250, 251, 252, 253, 254, 255 };
        public static int[] f_rough_red = new[] { 256, 257, 258, 259 };
        public static int[] f_rough_blue = new[] { 260, 261, 262, 263 };
        public static int[] f_rough_green = new[] { 264, 265, 266, 267 };
        public static int[] f_rough_cyan = new[] { 268, 269, 270, 271 };
        public static int[] f_rough_magenta = new[] { 272, 273, 274, 275 };
        public static int[] f_rough_brown = new[] { 276, 277, 278, 279 };
        public static int[] f_rough_lightgray = new[] { 280, 281, 282, 283 };
        public static int[] f_rough_darkgray = new[] { 284, 285, 286, 287 };
        public static int[] f_rough_lightblue = new[] { 288, 289, 290, 291 };
        public static int[] f_rough_lightgreen = new[] { 292, 293, 294, 295 };
        public static int[] f_rough_lightcyan = new[] { 296, 297, 298, 299 };
        public static int[] f_rough_lightred = new[] { 300, 301, 302, 303 };
        public static int[] f_rough_lightmagenta = new[] { 304, 305, 306, 307 };
        public static int[] f_rough_yellow = new[] { 308, 309, 310, 311 };
        public static int[] f_rough_white = new[] { 312, 313, 314, 315 };
        public static int[] f_sand = new[] { 316, 317, 318, 319, 320, 321, 322, 323 };
        public static int[] f_cobble_blood = new[] { 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335 };
        public static int[] f_marble_floor = new[] { 336, 337, 338, 339, 340, 341 };
        public static int[] f_sandstone_floor = new[] { 342, 343, 344, 345, 346, 347, 348, 349, 350, 351 };
        public static int[] f_volcanic_floor = new[] { 352, 353, 354, 355, 356, 357, 358 };
        public static int[] f_crystal_floor = new[] { 359, 360, 361, 362, 363, 364 };
        public static int[] f_grass = new[] { 365, 366, 367 };
        public static int[] f_grass_flowers_blue = new[] { 368, 369, 370 };
        public static int[] f_grass_flowers_red = new[] { 371, 372, 373 };
        public static int[] f_grass_flowers_yellow = new[] { 374, 375, 376 };
        public static int[] f_grass_p = new[] { 377, 378, 379, 380, 381, 382, 383, 384, 385 };
        public static int[] f_grass0_dirt_mix = new[] { 386, 387, 388 };
        public static int[] f_floor_nerves = new[] { 389, 390, 391, 392, 393, 394, 395 };
        public static int[] f_floor_nerves_blue = new[] { 396, 397, 398, 399, 400, 401, 402 };
        public static int[] f_floor_nerves_green = new[] { 403, 404, 405, 406, 407, 408, 409 };
        public static int[] f_floor_nerves_cyan = new[] { 410, 411, 412, 413, 414, 415, 416 };
        public static int[] f_floor_nerves_magenta = new[] { 417, 418, 419, 420, 421, 422, 423 };
        public static int[] f_floor_nerves_brown = new[] { 424, 425, 426, 427, 428, 429, 430 };
        public static int[] f_floor_nerves_lightgray = new[] { 431, 432, 433, 434, 435, 436, 437 };
        public static int[] f_floor_nerves_darkgray = new[] { 438, 439, 440, 441, 442, 443, 444 };
        public static int[] f_floor_nerves_lightblue = new[] { 445, 446, 447, 448, 449, 450, 451 };
        public static int[] f_floor_nerves_lightgreen = new[] { 452, 453, 454, 455, 456, 457, 458 };
        public static int[] f_floor_nerves_lightcyan = new[] { 459, 460, 461, 462, 463, 464, 465 };
        public static int[] f_floor_nerves_lightred = new[] { 466, 467, 468, 469, 470, 471, 472 };
        public static int[] f_floor_nerves_lightmagenta = new[] { 473, 474, 475, 476, 477, 478, 479 };
        public static int[] f_floor_nerves_yellow = new[] { 480, 481, 482, 483, 484, 485, 486 };
        public static int[] f_floor_nerves_white = new[] { 487, 488, 489, 490, 491, 492, 493 };
        public static int[] f_grass_pedestal_p = new[] { 494, 495, 496, 497, 498, 499, 500, 501, 502 };
        public static int[] f_pedestal_p = new[] { 503, 504, 505, 506, 507, 508, 509, 510, 511 };
        public static int[] f_dirt = new[] { 512, 513, 514 };
        public static int[] f_dirt_p = new[] { 515, 516, 517, 518, 519, 520, 521, 522, 523 };
        public static int[] f_tutorial_pad = new[] { 524 };
        public static int[] f_limestone = new[] { 525, 526, 527, 528, 529, 530, 531, 532, 533, 534 };
        public static int[] f_white_marble = new[] { 535, 536, 537, 538, 539, 540, 541, 542, 543, 544 };
        public static int[] f_sigil = new[] { 545, 546, 547, 548, 549, 550, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575 };
        public static int[] f_infernal = new[] { 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590 };
        public static int[] f_infernal_blank = new[] { 591 };
        public static int[] f_labyrinth = new[] { 592, 593, 594, 595 };
        public static int[] f_crypt_domino = new[] { 596, 597, 598, 599, 600, 601, 602, 603, 604, 605 };
        public static int[] f_iron = new[] { 606, 607, 608, 609, 610, 611 };
        public static int[] f_black_cobalt = new[] { 612, 613, 614, 615, 616, 617, 618, 619, 620, 621, 622, 623 };
        public static int[] f_frozen = new[] { 624, 625, 626, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636 };
        public static int[] f_demonic_red = new[] { 637, 638, 639, 640, 641, 642, 643, 644, 645 };
        public static int[] f_demonic_blue = new[] { 646, 647, 648, 649, 650, 651, 652, 653, 654 };
        public static int[] f_demonic_green = new[] { 655, 656, 657, 658, 659, 660, 661, 662, 663 };
        public static int[] f_demonic_cyan = new[] { 664, 665, 666, 667, 668, 669, 670, 671, 672 };
        public static int[] f_demonic_magenta = new[] { 673, 674, 675, 676, 677, 678, 679, 680, 681 };
        public static int[] f_demonic_brown = new[] { 682, 683, 684, 685, 686, 687, 688, 689, 690 };
        public static int[] f_demonic_lightgray = new[] { 691, 692, 693, 694, 695, 696, 697, 698, 699 };
        public static int[] f_demonic_darkgray = new[] { 700, 701, 702, 703, 704, 705, 706, 707, 708 };
        public static int[] f_demonic_lightblue = new[] { 709, 710, 711, 712, 713, 714, 715, 716, 717 };
        public static int[] f_demonic_lightgreen = new[] { 718, 719, 720, 721, 722, 723, 724, 725, 726 };
        public static int[] f_demonic_lightcyan = new[] { 727, 728, 729, 730, 731, 732, 733, 734, 735 };
        public static int[] f_demonic_lightred = new[] { 736, 737, 738, 739, 740, 741, 742, 743, 744 };
        public static int[] f_demonic_lightmagenta = new[] { 745, 746, 747, 748, 749, 750, 751, 752, 753 };
        public static int[] f_demonic_yellow = new[] { 754, 755, 756, 757, 758, 759, 760, 761, 762 };
        public static int[] f_demonic_white = new[] { 763, 764, 765, 766, 767, 768, 769, 770, 771 };
        public static int[] f_green_bones = new[] { 772, 773, 774, 775, 776, 777, 778, 779, 780, 781, 782, 783 };
        public static int[] f_woodground = new[] { 784, 785, 786, 787, 788, 789, 790, 791, 792 };
        public static int[] f_cage = new[] { 793, 794, 795, 796, 797, 798 };
        public static int[] f_etched = new[] { 799, 800, 801, 802, 803, 804 };
        public static int[] f_mosaic = new[] { 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820 };
        public static int[] f_lava = new[] { 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836 };
        public static int[] f_open_sea = new[] { 837, 838 };
        public static int[] f_deep_water = new[] { 839, 840 };
        public static int[] f_shallow_water = new[] { 841, 842 };
        public static int[] f_shallow_water_disturbance = new[] { 843, 844 };
        public static int[] f_deep_water_murky = new[] { 845, 846 };
        public static int[] f_shallow_water_murky = new[] { 847, 848 };
        public static int[] f_shallow_water_murky_disturbance = new[] { 849, 850 };
        public static int[] f_unused0_broken = new[] { 851, 852, 853, 854, 855, 856, 857, 858 };
        public static int[] f_shoals_deep_water = new[] { 859, 860, 861, 862, 863, 864, 865, 866, 867, 868, 869, 870 };
        public static int[] f_shoals_shallow_water = new[] { 871, 872, 873, 874, 875, 876, 877, 878, 879, 880, 881, 882 };
        public static int[] f_shoals_shallow_water_disturbance = new[] { 883, 884, 885 };
        public static int[] f_deep_water_wave_corner_p = new[] { 886, 887, 888, 889, 890, 891, 892, 893 };
        public static int[] f_deep_water_wave_p = new[] { 894, 895, 896, 897, 898, 899, 900, 901 };
        public static int[] f_shallow_water_wave_corner_p = new[] { 902, 903, 904, 905 };
        public static int[] f_shallow_water_wave_p = new[] { 906, 907, 908, 909 };
        public static int[] f_ink_wave_corner_p = new[] { 910, 911, 912, 913 };
        public static int[] f_ink_wave_p = new[] { 914, 915, 916, 917 };
        public static int[] f_ink = new[] { 918 };
        public static int[] f_liquefaction = new[] { 919, 920 };

        // floor sets
        public static int[][] fs_grey_dirt = new[] { f_grey_dirt , f_grey_dirt_b };
        public static int[][] fs_pebble = new[] { f_pebble, f_pebble_brown, f_pebble_blue, f_pebble_green, f_pebble_cyan, f_pebble_red, f_pebble_magenta, f_pebble_darkgrey, f_pebble_lightblue, f_pebble_lightgreen, f_pebble_lightcyan, f_pebble_lightred, f_pebble_lightmagenta, f_pebble_yellow, f_pebble_white };
        public static int[][] fs_rough = new[] { f_rough_red, f_rough_blue, f_rough_green, f_rough_cyan, f_rough_magenta, f_rough_brown, f_rough_lightgray, f_rough_darkgray, f_rough_lightblue, f_rough_lightgreen, f_rough_lightcyan, f_rough_lightred, f_rough_lightmagenta, f_rough_yellow, f_rough_white };
        public static int[][] fs_floor_nerves = new [] {f_floor_nerves, f_floor_nerves_blue, f_floor_nerves_green, f_floor_nerves_cyan, f_floor_nerves_magenta, f_floor_nerves_brown, f_floor_nerves_lightgray, f_floor_nerves_darkgray, f_floor_nerves_lightblue, f_floor_nerves_lightgreen, f_floor_nerves_lightcyan, f_floor_nerves_lightred, f_floor_nerves_lightmagenta, f_floor_nerves_yellow, f_floor_nerves_white};
        public static int[][] fs_everything = new[] { f_grey_dirt, f_grey_dirt_b, f_pebble, f_pebble_brown, f_pebble_blue, f_pebble_green, f_pebble_cyan, f_pebble_red, f_pebble_magenta, f_pebble_darkgrey, f_pebble_lightblue, f_pebble_lightgreen, f_pebble_lightcyan, f_pebble_lightred, f_pebble_lightmagenta, f_pebble_yellow, f_pebble_white, f_cave, f_mesh, f_mud, f_ice, f_lair, f_orc, f_moss, f_bog_green, f_acidic_floor, f_snake_a, f_snake_c, f_snake_d, f_swamp, f_spider, f_tomb, f_rect_gray, f_floor_vines, f_rough_red, f_rough_blue, f_rough_green, f_rough_cyan, f_rough_magenta, f_rough_brown, f_rough_lightgray, f_rough_darkgray, f_rough_lightblue, f_rough_lightgreen, f_rough_lightcyan, f_rough_lightred, f_rough_lightmagenta, f_rough_yellow, f_rough_white, f_sand, f_cobble_blood, f_marble_floor, f_sandstone_floor, f_volcanic_floor, f_crystal_floor, f_grass, f_grass_flowers_blue, f_grass_flowers_red, f_grass_flowers_yellow, f_grass0_dirt_mix, f_floor_nerves, f_floor_nerves_blue, f_floor_nerves_green, f_floor_nerves_cyan, f_floor_nerves_magenta, f_floor_nerves_brown, f_floor_nerves_lightgray, f_floor_nerves_darkgray, f_floor_nerves_lightblue, f_floor_nerves_lightgreen, f_floor_nerves_lightcyan, f_floor_nerves_lightred, f_floor_nerves_lightmagenta, f_floor_nerves_yellow, f_floor_nerves_white, f_dirt, f_tutorial_pad, f_limestone, f_white_marble, f_sigil, f_infernal, f_infernal_blank, f_labyrinth, f_crypt_domino, f_iron, f_black_cobalt, f_frozen, f_demonic_red, f_demonic_blue, f_demonic_green, f_demonic_cyan, f_demonic_magenta, f_demonic_brown, f_demonic_lightgray, f_demonic_darkgray, f_demonic_lightblue, f_demonic_lightgreen, f_demonic_lightcyan, f_demonic_lightred, f_demonic_lightmagenta, f_demonic_yellow, f_demonic_white, f_green_bones, f_woodground, f_cage, f_etched, f_mosaic, f_lava, f_open_sea, f_deep_water, f_shallow_water, f_shallow_water_disturbance, f_deep_water_murky, f_shallow_water_murky, f_shallow_water_murky_disturbance, f_shoals_deep_water, f_shoals_shallow_water, f_shoals_shallow_water_disturbance, f_ink, f_liquefaction };

        //Walls
        public static int[] w_brick_dark_1 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public static int[] w_brick_dark_2 = new[] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        public static int[] w_brick_dark_3 = new[] { 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 };
        public static int[] w_brick_dark_4 = new[] { 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55 };
        public static int[] w_brick_dark_5 = new[] { 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75 };
        public static int[] w_brick_dark_6 = new[] { 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95 };
        public static int[] w_brick_brown = new[] { 96, 97, 98, 99, 100, 101, 102, 103 };
        public static int[] w_brick_blue = new[] { 104, 105, 106, 107, 108, 109, 110, 111 };
        public static int[] w_brick_green = new[] { 112, 113, 114, 115, 116, 117, 118, 119 };
        public static int[] w_brick_cyan = new[] { 120, 121, 122, 123, 124, 125, 126, 127 };
        public static int[] w_brick_red = new[] { 128, 129, 130, 131, 132, 133, 134, 135 };
        public static int[] w_brick_magenta = new[] { 136, 137, 138, 139, 140, 141, 142, 143 };
        public static int[] w_brick_lightgray = new[] { 144, 145, 146, 147, 148, 149, 150, 151 };
        public static int[] w_brick_darkgray = new[] { 152, 153, 154, 155, 156, 157, 158, 159 };
        public static int[] w_brick_lightblue = new[] { 160, 161, 162, 163, 164, 165, 166, 167 };
        public static int[] w_brick_lightgreen = new[] { 168, 169, 170, 171, 172, 173, 174, 175 };
        public static int[] w_brick_lightcyan = new[] { 176, 177, 178, 179, 180, 181, 182, 183 };
        public static int[] w_brick_lightred = new[] { 184, 185, 186, 187, 188, 189, 190, 191 };
        public static int[] w_brick_lightmagenta = new[] { 192, 193, 194, 195, 196, 197, 198, 199 };
        public static int[] w_brick_yellow = new[] { 200, 201, 202, 203, 204, 205, 206, 207 };
        public static int[] w_brick_white = new[] { 208, 209, 210, 211, 212, 213, 214, 215 };
        public static int[] w_brick_brown_vines = new[] { 216, 217, 218, 219 };
        public static int[] w_relief_lightgray = new[] { 220, 221, 222, 223 };
        public static int[] w_relief_white = new[] { 224, 225, 226, 227 };
        public static int[] w_relief_darkgray = new[] { 228, 229, 230, 231 };
        public static int[] w_relief_blue = new[] { 232, 233, 234, 235 };
        public static int[] w_relief_green = new[] { 236, 237, 238, 239 };
        public static int[] w_relief_cyan = new[] { 240, 241, 242, 243 };
        public static int[] w_relief_red = new[] { 244, 245, 246, 247 };
        public static int[] w_relief_magenta = new[] { 248, 249, 250, 251 };
        public static int[] w_relief_brown = new[] { 252, 253, 254, 255 };
        public static int[] w_relief_yellow = new[] { 256, 257, 258, 259 };
        public static int[] w_relief_lightblue = new[] { 260, 261, 262, 263 };
        public static int[] w_relief_lightgreen = new[] { 264, 265, 266, 267 };
        public static int[] w_relief_lightcyan = new[] { 268, 269, 270, 271 };
        public static int[] w_relief_lightred = new[] { 272, 273, 274, 275 };
        public static int[] w_relief_lightmagenta = new[] { 276, 277, 278, 279 };
        public static int[] w_beehives = new[] { 280, 281, 282, 283, 284, 285, 286, 287, 288, 289 };
        public static int[] w_lair = new[] { 290, 291, 292, 293 };
        public static int[] w_orc = new[] { 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305 };
        public static int[] w_slime = new[] { 306, 307, 308, 309, 310, 311, 312, 313 };
        public static int[] w_slime_stone = new[] { 314, 315, 316, 317 };
        public static int[] w_tomb = new[] { 318, 319, 320, 321 };
        public static int[] w_vault = new[] { 322, 323, 324, 325 };
        public static int[] w_vault_stone = new[] { 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341 };
        public static int[] w_zot_blue = new[] { 342, 343, 344, 345 };
        public static int[] w_zot_green = new[] { 346, 347, 348, 349 };
        public static int[] w_zot_cyan = new[] { 350, 351, 352, 353 };
        public static int[] w_zot_red = new[] { 354, 355, 356, 357 };
        public static int[] w_zot_magenta = new[] { 358, 359, 360, 361 };
        public static int[] w_zot_brown = new[] { 362, 363, 364, 365 };
        public static int[] w_zot_lightgray = new[] { 366, 367, 368, 369 };
        public static int[] w_zot_darkgray = new[] { 370, 371, 372, 373 };
        public static int[] w_zot_lightblue = new[] { 374, 375, 376, 377 };
        public static int[] w_zot_lightgreen = new[] { 378, 379, 380, 381 };
        public static int[] w_zot_lightcyan = new[] { 382, 383, 384, 385 };
        public static int[] w_zot_lightred = new[] { 386, 387, 388, 389 };
        public static int[] w_zot_lightmagenta = new[] { 390, 391, 392, 393 };
        public static int[] w_zot_yellow = new[] { 394, 395, 396, 397 };
        public static int[] w_zot_white = new[] { 398, 399, 400, 401 };
        public static int[] w_wall_flesh = new[] { 402, 403, 404, 405, 406, 407, 408 };
        public static int[] w_transparent_flesh = new[] { 409, 410, 411, 412, 413, 414 };
        public static int[] w_wall_vines = new[] { 415, 416, 417, 418, 419, 420, 421 };
        public static int[] w_pebble_red = new[] { 422, 423, 424, 425 };
        public static int[] w_pebble_blue = new[] { 426, 427, 428, 429 };
        public static int[] w_pebble_green = new[] { 430, 431, 432, 433 };
        public static int[] w_pebble_cyan = new[] { 434, 435, 436, 437 };
        public static int[] w_pebble_magenta = new[] { 438, 439, 440, 441 };
        public static int[] w_pebble_brown = new[] { 442, 443, 444, 445 };
        public static int[] w_pebble_lightgray = new[] { 446, 447, 448, 449 };
        public static int[] w_pebble_darkgray = new[] { 450, 451, 452, 453 };
        public static int[] w_pebble_lightblue = new[] { 454, 455, 456, 457 };
        public static int[] w_pebble_lightgreen = new[] { 458, 459, 460, 461 };
        public static int[] w_pebble_lightcyan = new[] { 462, 463, 464, 465 };
        public static int[] w_pebble_lightred = new[] { 466, 467, 468, 469 };
        public static int[] w_pebble_lightmagenta = new[] { 470, 471, 472, 473 };
        public static int[] w_pebble_yellow = new[] { 474, 475, 476, 477 };
        public static int[] w_pebble_white = new[] { 478, 479, 480, 481 };
        public static int[] w_pebble_goldbrown = new[] { 482, 483, 484, 485 };
        public static int[] w_pebble_darkbrown = new[] { 486, 487, 488, 489 };
        public static int[] w_shoals_wall = new[] { 490, 491, 492, 493 };
        public static int[] w_brick_gray = new[] { 494, 495, 496, 497 };
        public static int[] w_stone_smooth = new[] { 498, 499, 500, 501 };
        public static int[] w_marble_wall = new[] { 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513 };
        public static int[] w_sandstone_wall = new[] { 514, 515, 516, 517, 518, 519, 520, 521, 522, 523 };
        public static int[] w_volcanic_wall = new[] { 524, 525, 526, 527, 528, 529, 530 };
        public static int[] w_volcanic_wall_blue = new[] { 531, 532, 533, 534, 535, 536, 537 };
        public static int[] w_crystal_wall = new[] { 538, 539, 540, 541, 542, 543, 544, 545, 546, 547, 548, 549 };
        public static int[] w_snake = new[] { 550, 551, 552, 553, 554, 555, 556, 557, 558, 559 };
        public static int[] w_spider = new[] { 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575 };
        public static int[] w_stone_gray = new[] { 576, 577, 578, 579 };
        public static int[] w_stone_white = new[] { 580, 581, 582, 583 };
        public static int[] w_stone_dark = new[] { 584, 585, 586, 587 };
        public static int[] w_stone_black_marked = new[] { 588, 589, 590, 591, 592, 593, 594, 595, 596 };
        public static int[] w_undead_lightgray = new[] { 597, 598, 599, 600 };
        public static int[] w_undead_white = new[] { 601, 602, 603, 604 };
        public static int[] w_undead_darkgray = new[] { 605, 606, 607, 608 };
        public static int[] w_undead_blue = new[] { 609, 610, 611, 612 };
        public static int[] w_undead_green = new[] { 613, 614, 615, 616 };
        public static int[] w_undead_cyan = new[] { 617, 618, 619, 620 };
        public static int[] w_undead_red = new[] { 621, 622, 623, 624 };
        public static int[] w_undead_magenta = new[] { 625, 626, 627, 628 };
        public static int[] w_undead_brown = new[] { 629, 630, 631, 632 };
        public static int[] w_undead_yellow = new[] { 633, 634, 635, 636 };
        public static int[] w_undead_lightblue = new[] { 637, 638, 639, 640 };
        public static int[] w_undead_lightgreen = new[] { 641, 642, 643, 644 };
        public static int[] w_undead_lightcyan = new[] { 645, 646, 647, 648 };
        public static int[] w_undead_lightred = new[] { 649, 650, 651, 652 };
        public static int[] w_undead_lightmagenta = new[] { 653, 654, 655, 656 };
        public static int[] w_church = new[] { 657, 658, 659, 660, 661 };
        public static int[] w_abyss = new[] { 662, 663, 664, 665, 666, 667, 668, 669 };
        public static int[] w_abyss_brown = new[] { 670, 671, 672, 673, 674, 675, 676, 677 };
        public static int[] w_abyss_green = new[] { 678, 679, 680, 681, 682, 683, 684, 685 };
        public static int[] w_abyss_cyan = new[] { 686, 687, 688, 689, 690, 691, 692, 693 };
        public static int[] w_abyss_blue = new[] { 694, 695, 696, 697, 698, 699, 700, 701 };
        public static int[] w_abyss_magenta = new[] { 702, 703, 704, 705, 706, 707, 708, 709 };
        public static int[] w_abyss_lightred = new[] { 710, 711, 712, 713, 714, 715, 716, 717 };
        public static int[] w_abyss_yellow = new[] { 718, 719, 720, 721, 722, 723, 724, 725 };
        public static int[] w_abyss_lightgreen = new[] { 726, 727, 728, 729, 730, 731, 732, 733 };
        public static int[] w_abyss_lightcyan = new[] { 734, 735, 736, 737, 738, 739, 740, 741 };
        public static int[] w_abyss_lightblue = new[] { 742, 743, 744, 745, 746, 747, 748, 749 };
        public static int[] w_abyss_lightmagenta = new[] { 750, 751, 752, 753, 754, 755, 756, 757 };
        public static int[] w_abyss_darkgray = new[] { 758, 759, 760, 761, 762, 763, 764, 765 };
        public static int[] w_abyss_lightgray = new[] { 766, 767, 768, 769, 770, 771, 772, 773 };
        public static int[] w_abyss_white = new[] { 774, 775, 776, 777, 778, 779, 780, 781 };
        public static int[] w_hell = new[] { 782, 783, 784, 785, 786, 787, 788, 789, 790, 791, 792 };
        public static int[] w_ice_wall = new[] { 793, 794, 795, 796, 797 };
        public static int[] w_icy_stone = new[] { 798, 799, 800, 801, 802 };
        public static int[] w_ice_block = new[] { 803, 804, 805, 806, 807 };
        public static int[] w_lab_rock = new[] { 808, 809, 810, 811 };
        public static int[] w_lab_stone = new[] { 812, 813, 814, 815, 816, 817 };
        public static int[] w_lab_metal = new[] { 818, 819, 820, 821, 822, 823, 824 };
        public static int[] w_crypt = new[] { 825, 826, 827, 828, 829, 830, 831, 832, 833, 834 };
        public static int[] w_crypt_metal = new[] { 835, 836, 837, 838, 839 };
        public static int[] w_cobalt_rock = new[] { 840, 841, 842, 843 };
        public static int[] w_cobalt_stone = new[] { 844, 845, 846, 847, 848, 849, 850, 851, 852, 853, 854, 855 };
        public static int[] w_catacombs = new[] { 856, 857, 858, 859, 860, 861, 862, 863, 864, 865, 866, 867, 868, 869, 870, 871 };
        public static int[] w_stone2_gray = new[] { 872, 873, 874, 875 };
        public static int[] w_stone2_dark = new[] { 876, 877, 878, 879 };
        public static int[] w_stone2_blue = new[] { 880, 881, 882, 883 };
        public static int[] w_stone2_green = new[] { 884, 885, 886, 887 };
        public static int[] w_stone2_cyan = new[] { 888, 889, 890, 891 };
        public static int[] w_stone2_red = new[] { 892, 893, 894, 895 };
        public static int[] w_stone2_magenta = new[] { 896, 897, 898, 899 };
        public static int[] w_stone2_brown = new[] { 900, 901, 902, 903 };
        public static int[] w_stone2_darkgray = new[] { 904, 905, 906, 907 };
        public static int[] w_stone2_yellow = new[] { 908, 909, 910, 911 };
        public static int[] w_stone2_lightblue = new[] { 912, 913, 914, 915 };
        public static int[] w_stone2_lightgreen = new[] { 916, 917, 918, 919 };
        public static int[] w_stone2_lightcyan = new[] { 920, 921, 922, 923 };
        public static int[] w_stone2_lightred = new[] { 924, 925, 926, 927 };
        public static int[] w_stone2_lightmagenta = new[] { 928, 929, 930, 931 };
        public static int[] w_stone2_white = new[] { 932, 933, 934, 935 };
        public static int[] w_transparent_wall = new[] { 936, 937, 938, 939, 940, 941, 942, 943, 944 };
        public static int[] w_transparent_stone = new[] { 945, 946, 947, 948, 949, 950, 951, 952, 953 };
        public static int[] w_mirrored_wall = new[] { 954 };
        public static int[] w_silver_wall = new[] { 955 };
        public static int[] w_metal_wall = new[] { 956, 957, 958, 959, 960, 961, 962, 963, 964 };
        public static int[] w_metal_wall_blue = new[] { 965, 966, 967, 968, 969, 970, 971, 972, 973 };
        public static int[] w_metal_wall_green = new[] { 974, 975, 976, 977, 978, 979, 980, 981, 982 };
        public static int[] w_metal_wall_cyan = new[] { 983, 984, 985, 986, 987, 988, 989, 990, 991 };
        public static int[] w_metal_wall_red = new[] { 992, 993, 994, 995, 996, 997, 998, 999, 1000 };
        public static int[] w_metal_wall_magenta = new[] { 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009 };
        public static int[] w_metal_wall_lightgray = new[] { 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018 };
        public static int[] w_metal_wall_darkgray = new[] { 1019, 1020, 1021, 1022, 1023 };

        // wall sets

        public static int[][] ws_everything = new[] { w_brick_dark_1, w_brick_dark_2, w_brick_dark_3, w_brick_dark_4, w_brick_dark_5, w_brick_dark_6, w_brick_brown, w_brick_blue, w_brick_green, w_brick_cyan, w_brick_red, w_brick_magenta, w_brick_lightgray, w_brick_darkgray, w_brick_lightblue, w_brick_lightgreen, w_brick_lightcyan, w_brick_lightred, w_brick_lightmagenta, w_brick_yellow, w_brick_white, w_brick_brown_vines, w_relief_lightgray, w_relief_white, w_relief_darkgray, w_relief_blue, w_relief_green, w_relief_cyan, w_relief_red, w_relief_magenta, w_relief_brown, w_relief_yellow, w_relief_lightblue, w_relief_lightgreen, w_relief_lightcyan, w_relief_lightred, w_relief_lightmagenta, w_beehives, w_lair, w_orc, w_slime, w_slime_stone, w_tomb, w_vault, w_vault_stone, w_zot_blue, w_zot_green, w_zot_cyan, w_zot_red, w_zot_magenta, w_zot_brown, w_zot_lightgray, w_zot_darkgray, w_zot_lightblue, w_zot_lightgreen, w_zot_lightcyan, w_zot_lightred, w_zot_lightmagenta, w_zot_yellow, w_zot_white, w_wall_flesh, w_transparent_flesh, w_wall_vines, w_pebble_red, w_pebble_blue, w_pebble_green, w_pebble_cyan, w_pebble_magenta, w_pebble_brown, w_pebble_lightgray, w_pebble_darkgray, w_pebble_lightblue, w_pebble_lightgreen, w_pebble_lightcyan, w_pebble_lightred, w_pebble_lightmagenta, w_pebble_yellow, w_pebble_white, w_pebble_goldbrown, w_pebble_darkbrown, w_shoals_wall, w_brick_gray, w_stone_smooth, w_marble_wall, w_sandstone_wall, w_volcanic_wall, w_volcanic_wall_blue, w_crystal_wall, w_snake, w_spider, w_stone_gray, w_stone_white, w_stone_dark, w_stone_black_marked, w_undead_lightgray, w_undead_white, w_undead_darkgray, w_undead_blue, w_undead_green, w_undead_cyan, w_undead_red, w_undead_magenta, w_undead_brown, w_undead_yellow, w_undead_lightblue, w_undead_lightgreen, w_undead_lightcyan, w_undead_lightred, w_undead_lightmagenta, w_church, w_abyss, w_abyss_brown, w_abyss_green, w_abyss_cyan, w_abyss_blue, w_abyss_magenta, w_abyss_lightred, w_abyss_yellow, w_abyss_lightgreen, w_abyss_lightcyan, w_abyss_lightblue, w_abyss_lightmagenta, w_abyss_darkgray, w_abyss_lightgray, w_abyss_white, w_hell, w_ice_wall, w_icy_stone, w_ice_block, w_lab_rock, w_lab_stone, w_lab_metal, w_crypt, w_crypt_metal, w_cobalt_rock, w_cobalt_stone, w_catacombs, w_stone2_gray, w_stone2_dark, w_stone2_blue, w_stone2_green, w_stone2_cyan, w_stone2_red, w_stone2_magenta, w_stone2_brown, w_stone2_darkgray, w_stone2_yellow, w_stone2_lightblue, w_stone2_lightgreen, w_stone2_lightcyan, w_stone2_lightred, w_stone2_lightmagenta, w_stone2_white, w_transparent_wall, w_transparent_stone, w_mirrored_wall, w_silver_wall, w_metal_wall, w_metal_wall_blue, w_metal_wall_green, w_metal_wall_cyan, w_metal_wall_red, w_metal_wall_magenta, w_metal_wall_lightgray, w_metal_wall_darkgray };

        private static System.Random rng;

        static Tileset()
        {
            rng = new System.Random();
        }

        public static int[] GetRandomSet(int[][] sets)
        {
            return sets[rng.Next(sets.Length)];
        }

        public static int GetRandom(int[] i)
        {
            return i[rng.Next(i.Length)];
        }

        public static int[] Merge(params int[][] sets)
        {
            var m = new List<int>();
            foreach (var i in sets)
            {
                m.AddRange(i);
            }
            return m.ToArray();
        }
    }
}