Set-ExecutionPolicy -ExecutionPolicy Unrestricted
invoke-expression (invoke-webrequest https://chocolatey.org/install.ps1).content
choco install typescript
choco  install nodejs
choco install Nswagstudio
npm install chai mocha ts-node @types/chai @types/mocha --save-dev
# test that tsc and npx are valid command on the command line.

explorer http://bit.ly/2SUVXqn