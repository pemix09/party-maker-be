#commit message:
$CommitMessage = "Ban controller creation"

#add all files to commit:
git add .

#add new commit and message to it:
git commit -m $CommitMessage

#push newly created commit to github:
git push

#and upload it to heroku(envoke other script):
& '.\UploadToHeroku.ps1'