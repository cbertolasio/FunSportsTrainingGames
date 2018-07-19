FROM node:8.9-alpine
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
COPY ["package.json", "package-lock.json*", "npm-shrinkwrap.json*", "./"], /usr/src/app
RUN npm install
COPY . /usr/src/app
EXPOSE 4200
CMD npm start