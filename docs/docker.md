# Docker
In this section, we will document the decisions related with the dockerize of our tests and application. The first step is the selection of the images we are going to try. In this case, we select ubuntu 20.04 and alpine 3.15 as an initial approach, and later on, we will try the image Windows offers.

Since our project uses .Net 5.0, we need some basic dependencias as the .NET runtime and SDK, so we will install them whenever we need it. The minimum amount of space required is about 400 MB, as you can see.
![NET environment size](images/dotnetSize.png)

## [Ubuntu 20.04 Image](dockers/ubuntu.20.04.dockerfile)
We started using a default ubuntu image. In this case, we selected the 20.04 one because it's the one being used in the development laptop.

Since it was the first image, we faced tons of issues in order to run the tests. The first one was that Nuke was not installing properly the .NET environment, so we had to add a RUN command to install everything. First, we installed "wget" to be able to download the file that contains the information to find the dotnet repositories with apt. The, we just added the sentences to download it. In this first image, we tried to move up and down some layers (like "WORKDIR" and "COPY"). Also, we tried to copy just the expected files.   

Finally, we dismissed those last change for a few reasons. 
- The size of the image is so big that adding a few megabytes is negligible.![Ubuntu20.04 image with its size](images/DockerUbuntu20.04Image.png)
- The time recompiling the layers changed a little bit, but changing the workdir leads to some wrong paths and copying first the project will lead in the future to recompile the following layers, even though right now it's not happening (because we are not modifying the code).

## [Alpine 3.15 Image](dockers/alpine.3.15.dockerfile)
This alpine image has practically nothing installed with it, so it is a nice start point to research after the ubuntu image results. After a few trials and errors creating the needed environment, reading [the documentation](https://docs.microsoft.com/en-gb/dotnet/core/install/linux-scripted-manual) shows that we need to make sure that some dependencies are installed, like "libstdc++6". Since our task manager (Nuke) takes care of the installation of the .NET environment, we need to make sure that everything is in place. Since Nuke uses cURL to download some files, we add the dependency to the RUN sentence and we just execute the image.

Finally, we have our image created with a size of 1.64Gb as you can see in the next image. 
![Alpine image with its size](images/DockerAlpineImage.png)
As you can see, it's a little bit smaller than the ubuntu one as expected.

## [DotNet 5.0 Image](dockers/dotnet.sdk5.0.dockerfile)
This image is the one created and mantained by Microsoft. As a result, it contains by default all the dependencies that this project needs. Furthermore, the default configuration is enough for Nuke to work, so it skips the installation of the .NET environment. As a result, this image is the most compacted one.![DotNet5.0 image with its size](images/DockerDotNet5Image.png)


## Conclusions
From the very beginning, the DotNet image is the one which seems to be the best for this project. It is literally created by Microsoft, which develops and maintains C#, and it contains everything .NET needs to run. But I hoped to be able to create a similar image from scratch similar to the DotNet one, to have more control over the layers. 

After every try and error, and tons of trials, I have to admit that the DotNet image is the best fit for this project and, consequently, the one chosen.

## Side notes related with images
As we said in the above paragraph, the initial intention was to be able to create a custom image from scratch and have control over the layers and its order. For this specific reason, the first attempt was to create an image from scratch.![Scratch image](images/scratchDockerfile.png)

As expected, this image was pretty small but it was really hard to make it work. [The documentation](https://hub.docker.com/_/scratch) states that this image should be use just as base for debian images or for single binaries and its dependencies, which is not the case of this project. Additionally, we checked that the [ubuntu image chosen](https://github.com/tianon/docker-brew-ubuntu-core/blob/570d5970a8b18bc772ad2c3eb1ce8fd0887d991a/focal/Dockerfile) to investigate is just a scratch image with ubuntu, so we consider that the alpine image and that one are enough for the scope of this project and could show similar results at the end.

# Docker Hub
In this section, we will describe the steps to upload the image to Docker Hub. We are going to skip the login steps since they are pretty similar to all the online platforms. Our name was not taken so we could just create our account with our GitHub nickname.

So the first step to upload our container will be to create a new public repository with the name of our project as you can see in the image below.
![Create Docker Hub project](images/DH-CreateRepository.png)

When the repository is created, it's time to go to our local machine and log in to Docker Hub from the console with this command.
    
    docker login -u samius1 -p <password>

After the log in is successful, there is only one command left to upload the image to Docker Hub.

    docker push samius1/succesales:latest

Finally, if you go to the repository page, there will be a new image uploaded with the tag you selected.
![Image uploaded to Docker Hub](images/DH-ImageUploaded.png)