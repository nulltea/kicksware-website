# [![repo logo][]][Kicksware url]

<p align="center">
	<a href="https://www.kicksware.com">
		<img src="https://img.shields.io/website?label=Visit%20website&down_message=unavailable&up_color=teal&up_message=kicksware.com%20%7C%20online&url=https%3A%2F%2Fhealth.kicksware.com/ready">
	</a>
</p>

[![c# badge]](https://docs.microsoft.com/dotnet/csharp)&nbsp;
[![js badge]][jamstack]&nbsp;
[![asp.net badge]][asp.net core]&nbsp;
[![kubernetes badge]](https://kubernetes.io)&nbsp;
[![maintainability badge]][maintainability source]&nbsp;
[![license badge]](https://www.gnu.org/licenses/agpl-3.0)

## Overview

_**Kicksware web application**_ provides a publicly accessible visual user interface to interact with the Kicksware sneaker resale platform.

It introduces a visually satisfying, convenient, new way to both buy and sell new or used sneakers, create your own great wishlist and collaborate with other sneakerheads around the globe.

Sounds interesting? Consider visiting [**kicksware.com**][kicksware url] to find out for yourself!

## Table of contents

* [Overview](#overview)
* [Table of contents](#table-of-contents)
* [Back-end design](#back-end-design)
* [Front-end design](#front-end-design)
* [Requirements](#requirements)
* [Deployment](#deployment)
* [Wrap Up](#wrap-up)
* [License](#license)

## Back-end design

Being a part of microservice based it system brings flexibility into web application development process while eliminating vendor and technology lock-in.

Globally Kicksware's back-end logic is written in Go programming language as a set of microservices. Unfortunately, Go isn't currently as great for front-end development as it is for back-end. Luckily, MSA is designed to deal with such problems so your client and server-side logic can be implemented with different languages. Moreover, every service can be build with its own set of languages and technologies as long as it's possible to maintain all of them.

On these terms, Kicksware web app has been developed with the use of **C#** language with its native [**Asp.Net Core**][asp.net core], as it is a cross-platform, enterprise-ready, open-source framework for building modern, cloud-enabled, web apps.

[Model-View-Controller (MVC)][mvc pattern] is a design pattern used for developing Kicksware user interfaces via dividing its program logic into three interconnected elements for defining data, visual markup and business-rules respectively.

## Front-end design

What's tricky about doing front-end in a microservice architecture is the fact that users actually don’t care how well the backend is divided into atomic, loose coupled microservices. The question for them is how well it's integrated with their browser.

To make your experience of surfing through tons of great designed sneaker models even better, Kicksware relies on _client-side **J**avaScript, reusable **A**PIs, and prebuilt **M**arkup_. The term hiding in the names of these technologies, is [_**JAM**stack_][jamstack]. It’s a new way of building web apps that delivers better performance, higher security, lower cost of scaling, and overall better developer experience.

The last but not the least is the visual design itself. To achieve a modern, appealing look while providing a user-friendly intuitive interface, Kicksware adopts Google's Material design.

[![kicksware browser][]][Kicksware url]

## Requirements

To ensure proper, full-fledge performance of Kicksware web application, all [Gateway][gateway repo], [Tool Stack][tool-stack repo] and [API][api repo] components must be deployed first. Otherwise, the website's public accessibility will not be possible without [Traefik proxy][gateway repo].

It's also worth noticing that the app will actually work without a dedicated [API][api repo] and [database][tool-stack repo], although in such conditions it will be useless.

## Deployment

Kicksware web app can be deployed using the following methods:

1. **Docker Compose file**

   This method require single dedicated server with installed both [`docker`][docker-compose] and [`docker-compose`][docker-compose] utilities.

   Compose [configuration file][compose config] can be found in the root of the project. This file already contains setting for reverse proxy routing and load balancing.

   Gitlab CI deployment pipeline [configuration file][ci compose config] for compose method can be found in `.gitlab` directory.

2. **Kubernetes Helm charts**

   Deployment to Kubernetes cluster is the default and desired way.

   For more flexible and easier deployment [Helm package manager][helm] is used. It provides a simple, yet elegant way to write pre-configured, reusable Kubernetes resource configuration using YAML and Go Templates (or Lua scripts). Helm packages are called `charts`.

   Web application [deployment chart][helm chart] directory can be found in the root of the project.

   Helm chart configuration already contains configuration of [Traefik IngressRoute][ingress route] [Custom Resource Definition (CRD)][k8s crd] for reverse proxy routing and load balancing.

   Gitlab CI deployment pipeline [configuration file][ci k8s config] for K8s method can be found in the root of the project.

## Wrap Up

**Kicksware web app** is a public exposure of a noncommercial, research project dedicated to showcasing the use of modern technologies in the context of the fashion industry.

While being a part of a distributed, diverse, microservice-based software infrastructure, it exploits the flexibility and independence provided to it by the architecture.

Although this website can't satisfy the potential buyer with a pair of new Nike's, it still tries to deliver the best usage experience with the help of both innovative web frameworks and proven graphical design solutions.

## More
See [other Kicksware project repositories][kicksware-main-repo].

## License

Licensed under the [GNU AGPL-3.0][license file].

[repo logo]: https://raw.githubusercontent.com/timoth-y/kicksware-web-app/master/assets/repo-logo.png
[kicksware url]: https://www.kicksware.com

[Website badge]: https://img.shields.io/website?label=Visit%20website&down_message=unavailable&up_color=teal&up_message=kicksware.com%20%7C%20online&url=https%3A%2F%2Fkicksware.com
[c# badge]: https://img.shields.io/badge/Code-C%23-informational?style=flat&logo=c-sharp&logoColor=white&color=1E9E25
[js badge]: https://img.shields.io/badge/Code-JavaScript-informational?style=flat&logo=javascript&logoColor=white&color=F7E018
[commit activity badge]: https://img.shields.io/github/commit-activity/m/timoth-y/kicksware-web-app?label=Commit%20activity&color=teal
[repo commit activity]: https://github.com/timoth-y/kicksware-web-app/graphs/commit-activity
[lines counter]: https://img.shields.io/tokei/lines/github/timoth-y/kicksware-web-app?color=teal&label=Lines
[asp.net badge]: https://img.shields.io/badge/Framework-ASP.NET%20Core-informational?style=flat&logo=.net&logoColor=white&color=teal
[license badge]: https://img.shields.io/badge/License-AGPL%20v3-blue.svg?color=teal
[architecture badge]: https://img.shields.io/badge/Architecture-Microservices-informational?style=flat&logo=opslevel&logoColor=white&color=teal
[kubernetes badge]: https://img.shields.io/badge/DevOps-Kubernetes-informational?style=flat&logo=kubernetes&logoColor=white&color=316DE6
[gitlab badge]: https://img.shields.io/badge/CI-Gitlab_CE-informational?style=flat&logo=gitlab&logoColor=white&color=FCA326
[web-app pipeline]: https://ci.kicksware.com/kicksware/web-app/badges/master/pipeline.svg?key_text=Web%20App%20|%20pipeline&key_width=115
[maintainability badge]: https://api.codeclimate.com/v1/badges/225b52efad204b10fc2d/maintainability
[maintainability source]: https://codeclimate.com/github/timoth-y/kicksware-web-app/maintainability

[asp.net core]: https://dotnet.microsoft.com/apps/aspnet
[mvc pattern]: https://www.codecademy.com/articles/mvc
[jamstack]: https://jamstack.org
[material design]: https://material.io/design

[kicksware browser]: https://raw.githubusercontent.com/timoth-y/kicksware-web-app/master/assets/kicksware-browser.png

[api repo]: https://github.com/timoth-y/kicksware-api
[gateway repo]: https://github.com/timoth-y/kicksware-gateway
[tool-stack repo]: https://github.com/timoth-y/kicksware-tool-stack

[docker-desktop]: https://docs.docker.com/desktop/
[docker-compose]: https://docs.docker.com/compose/
[compose config]: https://github.com/timoth-y/kicksware-web-app/blob/master/docker-compose.yml
[ci compose config]: https://github.com/timoth-y/kicksware-web-app/blob/master/.gitlab/.gitlab-ci.compose.yml
[ci k8s config]: https://github.com/timoth-y/kicksware-web-app/blob/master/.gitlab-ci.yml
[k8s crd]: https://kubernetes.io/docs/concepts/extend-kubernetes/api-extension/custom-resources/
[ingress route]: https://docs.traefik.io/routing/providers/kubernetes-crd/

[helm]: https://helm.sh/
[helm chart]: https://github.com/timoth-y/kicksware-web-app/tree/master/webapp-chart

[kicksware-main-repo]: https://github.com/timoth-y/kicksware-platform#components

[license file]: https://github.com/timoth-y/kicksware-web-app/blob/master/LICENSE
