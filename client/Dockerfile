#FROM node:alpine AS build-stage
#WORKDIR /app
#
#COPY package*.json ./
#RUN npm install
#COPY . . 
#RUN npm run dev
#    
#EXPOSE 80
#CMD ["npm", "run", "dev"]

#FROM node:alpine AS build-stage
#WORKDIR /app
#COPY package*.json ./
#RUN npm install
#COPY . . 
#RUN npm run build
#
#FROM nginx:alpine
#COPY --from=build-stage /app/dist /usr/share/nginx/html
#COPY nginx.conf /etc/nginx/conf.d/default.conf
#
#EXPOSE 80
#
#CMD ["nginx", "-g", "daemon off;"]

# Use the official Node.js runtime as the base image
FROM node:18 as build

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the entire application code to the container
COPY . .

# Build the React app for production
RUN npm run build

# Use Nginx as the production server
FROM nginx:alpine

# Copy the built React app to Nginx's web server directory
COPY --from=build /app/dist /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 80 for the Nginx server
EXPOSE 80

# Start Nginx when the container runs
CMD ["nginx", "-g", "daemon off;"]