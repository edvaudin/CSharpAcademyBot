# CSharpAcademyBot

This bot's primary feature is inspired by DamnitDan's idea to award titles to members of the C# Academy discord that have been helpful and provide useful support to people learning.


## Current Features

The bot will listen to reactions added to new messages. If it is a green checkbox, then it will award +1 reputation to the author via a MySQL database connection. If a green checkbox is removed from a message, the author will lose 1 reputation accordingly. Each time reputation changes, a check is made as to whether the author has reached a milestone that earns them a title. The titles are stored on the MySQL database and the author will be awarded the highest title they have earned.

## Improvements to be made

- Revoke roles on reputation loss
- Revoke any superceded roles
- Set up announcements for when a member earns a new title (optional)
