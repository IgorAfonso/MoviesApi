"use client";

import profileBlue from "@/public/profile_default_icons/profile_blue.jpg";
import profileGreen from "@/public/profile_default_icons/profile_green.jpg";
import profileRed from "@/public/profile_default_icons/profile_red.jpg";
import profileYellow from "@/public/profile_default_icons/profile_yellow.jpg";
import Image from "next/image";
import Button from "./ProfileButton";

export default function ProfileChoise() {
  const handleClick = () => {
    alert("Botão clicado!");
  };

  const profileObject = [
    {
      name: "user1",
      image: profileBlue,
    },
    {
      name: "user2",
      image: profileGreen,
    },
    {
      name: "user3",
      image: profileRed,
    },
    {
      name: "user4",
      image: profileYellow,
    },
  ];

  const processedProfiles = profileObject.map((user) => {
    return (
      <div key={user.name}>
        <Image src={user.image} alt="PopFlix Image" width={180} height={50} />
        <p>{user.name}</p>
      </div>
    );
  });

  return (
    <div className="text-center min-h-screen text-[30px] md:text-[50px] font-netflix">
      <p>Quem vai assistir ?</p>
      <div className="flex md:flex-row justify-center">
        {
          <Button
            className="flex flex-col md:flex-row gap-5 justify-center my-20 text-center font-netflix text-[33px]"
            onClick={handleClick}
          >
            {processedProfiles}
          </Button>
        }
      </div>
      <div>
        <Button
          className="border-2 md:py-2 md:px-4 md:mb-20 border-solid md:border-gray-50 md:text-gray-50 font-netflix text-[10px] md:text-[26px]"
          onClick={handleClick}
        >
          GERENCIAR PERFIS
        </Button>
      </div>
    </div>
  );
}
