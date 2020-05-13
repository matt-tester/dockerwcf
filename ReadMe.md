# Demo Containerization

This demonstration project takes an extremely basic WCF and "dockerizes" it so that it can run as a Docker container. 

The project itself retains the existing developer experience of building and debugging on the developer's machine. At this stage, containers are not used.

## Requirements

* Windows 10 Pro/Enterprise
  * Hyper-V enabled
* Docker for Windows installed
  * https://docs.docker.com/docker-for-windows/install/
* 4Gb RAM minimum
* At least 16Gb of disk space

## Build and run

> Ensure that *Docker for Windows* is running and is set run Windows containers. 

Assuming this code is downloaded to your machine via a `git clone`, we first need the container image to be built. 

1. Open a command prompt (e.g. Powershell) to the `WcfHelloWorld` folder
2. Build the Docker image
   
    The following command will build the code and produce an image in our local Docker image cache named `wcfhelloworld`:

    ```
    PS> docker build -t wcfhelloworld .
    ```

    Once complete, the image should be shown given:

    ```
    PS> docker images

    REPOSITORY                               TAG                 IMAGE ID            CREATED             SIZE
   wcfhelloworld                            latest              12ba287f7243        5 seconds ago       6.97GB
   <none>                                   <none>              d41bb04727cf        7 seconds ago       8.99GB
    ``` 

3. Run a new instance of the container
   
   A new container instance can be created from the image, using the name of the image, as well as the port to use (in this example port `51468`):

   ```
   PS> docker run -d -p 51468:80 wcfhelloworld
   ```

   To verify the container is running:

   ```
   PS> docker container ls

   CONTAINER ID        IMAGE               COMMAND                   CREATED             STATUS              PORTS                            NAMES
   3c612476ac40        wcfhelloworld       "powershell C:\\servi…"   4 minutes ago       Up 4 minutes        808/tcp, 0.0.0.0:51468->80/tcp   eager_wilson
   ```

4. Connect to the WCF service in the browser.

   We created this container, mapping on port `51468`, so we should browse to:
   
   ```
   http://localhost:51468/wcfhelloworld.svc
   ```
   Or to get the WSDL

   ```
   http://localhost:51468/wcfhelloworld.svc?singleWsdl
   ```

## Build and run without cloning

Docker can accept a Git url to build from. To create the Docker image directly from this repository, we can use:

```
PS> docker build -t wcfdemofromgit https://github.com/matt-tester/dockerwcf.git#:wcfhelloworld
```

This will produce an image with the name `wcfdemofromgit`.

> The url format on the end of the Git repo url, is specifying the folder to find the docker file using the format `#{branch}:{folder}`. To use `master`, we can leave it blank, hence `#:{folder}` will use the `master` branch.

## Managing containers

The container can be stopped and started using the following commands, identifying the container instance y it's `Container ID` (from the `docker container ls` command). Note that the full id does not need to be used, just enough to uniquely identify.


Command to stop the container (given example `Container ID` above)
```
PS> docker stop 3c6
```

To restart the same container
```
PS> docker start 3c6
```

> Note that `docker run` is possibly badly named, as it will create a new container instance each time. Once a container instance is created, it can actually just be stopped and stated.

### How to look at the contents of the container?

We can open a command prompt into a running container using the `exec` Docker command.

```
PS> docker exec -it 3c6 "powershell"

Windows PowerShell
Copyright (C) Microsoft Corporation. All rights reserved.

Try the new cross-platform PowerShell https://aka.ms/pscore6

PS C:\inetpub\wwwroot> 
```

> The `-it` switch effectively means "interactive terminal". Without this, the command (`"powershell"`) would run, but immediately complete.

You're now in the powershell prompt in the container to be able to inspect and run commands.

To return to the host machine command prompt, use `exit`.

```
PS C:\inetpub\wwwroot> exit
```


## Quick Reference Useful Docker Commands
* View images in local cache
    ```
    PS> docker images
    ```
* View running containers:
    ```
    PS> docker container ls
    ```
* View all containers (including stopped)
    ```
    PS> docker container ls --all
    ```
* Delete all stopped containers (NB: image remains)
    ```
    PS> docker container prune
    ```
* Build a new verion of the demo image
    ```
    PS> docker build -t {tag_name_to_give_image} .
    ```
* Run a new instance of the container on a different port
    ```
    PS> docker run -d -p {host_port}:80 {name_to_give_container_instance}
    ```
