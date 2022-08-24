#heroku login
heroku login
heroku container:login

#build docker container
docker build -t party-maker-be .

#push newly created container to heroku:
heroku container:push -a party-maker-be web

#and release it:
heroku container:release -a party-maker-be web