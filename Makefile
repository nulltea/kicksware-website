web-app:
	docker-compose down;
	docker-compose build;
	docker-compose up -d;

styles:
	cd wwwroot;
	mkdir -p styles/css2;
	for dir in $(shell ls styles/less); do \
		mkdir -p styles/css/$$dir; \
		lessc-each styles/less/$$dir styles/css/$$dir; \
	done;
	cd ..;