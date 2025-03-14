# Тестовое для Corteos 
## От Руслана Буляккулова
## https://hh.ru/resume/8941f5bfff0d0b20cc0039ed1f657730793048

### Стркутура базы в db/init.sql

Автозапуск можно реализовать через cron, например:

```
crontab -e
0 3 * * * /usr/bin/docker-compose -f путь-до-docker-compose.yml up -d --build >> /var/log/cron.log 2>&1
5 3 * * * /usr/bin/docker-compose -f путь-до-docker-compose.yml down >> /var/log/cron.log 2>&1
```
убрав цикл в Program.cs и убрав в docker-compose.yml параметр restart: always в app
