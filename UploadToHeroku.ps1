#check if user is logged in
heroku auth:whoami
if($? -eq $false){
    heroku login
}

#login to heroku container -> docker must be turned on
heroku container:login
if($? -eq $false){
    Write-Error "Docker has to be turned on!"
}
#if user logged in and docker is running
else{
    #build docker container
    docker build -t party-maker-be .

    #push newly created container to heroku:
    heroku container:push -a party-maker-be web

    #and release it:
    heroku container:release -a party-maker-be web
}
