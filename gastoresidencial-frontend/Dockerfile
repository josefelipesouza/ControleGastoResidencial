# Etapa 1 — Build da aplicação React (Vite)
FROM node:20-alpine AS build

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build


# Etapa 2 — Nginx para servir o build
FROM nginx:stable-alpine

# Remove a config default do nginx
RUN rm /etc/nginx/conf.d/default.conf

# Copia nossa configuração SPA
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia o build do Vite para a pasta do nginx
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
