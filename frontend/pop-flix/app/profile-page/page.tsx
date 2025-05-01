import Image from "next/image";
import myImage from "@/public/PopFlixIcon.png";
import ProfileChoise from "@/components/ProfileChoise/ProfileChoise";

export default function ProfilePage() {
  return (
    <div className="bg-black bg-cover max-h-screen md:overflow-hidden justify-center align-center md:justify-start">
      <Image src={myImage} alt="PopFlix Image" width={150} height={40} />
      <ProfileChoise />
    </div>
  );
}
