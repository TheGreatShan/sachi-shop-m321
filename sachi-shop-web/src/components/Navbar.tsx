export default function Navbar() {
  return (
    <div className="p-5 bg-blue-500 text-white grid grid-cols-6 fixed top-0 left-0 w-full z-10 items-center">
      <div className="col-span-1 flex gap-5">
        <img src="/shop-logo.svg" alt="logo" height={25} width={25} />
        Sachi Shop
      </div>
      <div className="col-span-4"></div>
      <div className="col-span-1 justify-self-end grid grid-cols-2 gap-5">
        <img src="/cart.svg" alt="profile" height={27} width={27} />
        <img src="/profile.svg" alt="profile" height={25} width={25} />
      </div>
    </div>
  );
}
