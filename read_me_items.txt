You have to check few values of your pack-items on your guild-market:
1. Class of items
2. (Not often) Soulbound items
3. Price of items

###File which data of pack-items should be ALWAYS saved as items.txt in the main folder (where main .exe exists [not shorcut])!
###Important is you have to have at least constant class/soulbound + obviously always constant price!
###(symbol of '*' means that it can have any item-class or any soulbound - price must be constant!)

And then you have to write it as on this example:
Example 1:

* 30791 100000
This example show's that on your guild market is always item which is an pack and have all the time soulbound to player ID 30791.
Price of the pack is 100k and class-items is random (program wont search about class-items, only soulbounds. It works when some of items are soulbound to player named for example: "100k")

Example 2: 
item-i-27-1 * 100000
The another example shows that on your guild market is always item which is an pack and have all the time class item-i-27-1 but dont have constant soulbound player. Price of this pack is 100k.

Wrong example 3:
* * 100000
This example is non valid. Item have to get at least one of 'id'. Soulbound or class. In other place program would buy all stuff which is placed on guild-market for price 100k.
It creates very fast many of problemss ;) Already tested it on my few guilds :) Wasn't playing there anymore because of kick :))