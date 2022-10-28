#heroku login
#if(heroku whoami -eq null)
sudo heroku login
sudo heroku container:login

#build docker container
sudo docker build -t party-maker-be .

#push newly created container to heroku:
sudo heroku container:push -a party-maker-be web

#and release it:
sudo heroku container:release -a party-maker-be web