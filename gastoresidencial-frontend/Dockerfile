FROM node:20-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine
# Copia o build do vite para a pasta do nginx
COPY --from=build /app/dist /usr/share/nginx/html
# Expõe a porta padrão do nginx
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]