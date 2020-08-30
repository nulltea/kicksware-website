web-app:
	docker-compose -f web-app-service/docker-compose.yml down;
	docker-compose -f web-app-service/docker-compose.yml build;
	docker-compose -f web-app-service/docker-compose.yml push web-app;
	docker-compose -f web-app-service/docker-compose.yml up -d;

styles:
	mkdir web-app-service/Web/wwwroot/styles/css;
	for dir in web-app-service/Web/wwwroot/styles/less/*; do \
		lessc-each web-app-service/Web/wwwroot/styles/less/$(basename ${dir}) web-app-service/Web/wwwroot/styles/css/$(basename ${dir}); \
	done;