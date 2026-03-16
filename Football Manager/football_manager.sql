-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 16, 2026 at 06:48 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `football_manager`
--

-- --------------------------------------------------------

--
-- Table structure for table `clubs`
--

CREATE TABLE `clubs` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `city` varchar(100) DEFAULT NULL,
  `stadium` varchar(100) DEFAULT NULL,
  `founded_year` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `clubs`
--

INSERT INTO `clubs` (`id`, `name`, `city`, `stadium`, `founded_year`) VALUES
(1, 'Левски София', 'София', 'Стадион Георги Аспарухов', 1914),
(2, 'Лудогорец', 'Разград', 'Хювефарма Арена', 2001),
(3, 'ЦСКА София', 'София', 'Българска армия', 1948),
(4, 'Черно море', 'Варна', 'Тича', 1913);

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(11) NOT NULL,
  `club_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `birth_date` date NOT NULL,
  `position` enum('GK','DF','MF','FW') NOT NULL,
  `shirt_number` int(11) DEFAULT NULL,
  `status` enum('Active','Injured','Suspended') DEFAULT 'Active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`id`, `club_id`, `full_name`, `birth_date`, `position`, `shirt_number`, `status`) VALUES
(1, 1, 'Мартин Луков', '1993-07-05', 'GK', 78, 'Active'),
(2, 1, 'Майкон', '1999-02-10', 'DF', 3, 'Active'),
(3, 1, 'Кристиан Макун', '2000-03-05', 'DF', 4, 'Active'),
(4, 1, 'Стипе Вуликич', '2001-01-01', 'DF', 6, 'Active'),
(5, 1, 'Алдаир Невеш', '1999-05-10', 'DF', 21, 'Active'),
(6, 1, 'Карлос Охене', '1992-07-21', 'MF', 8, 'Active'),
(7, 1, 'Асен Митков', '2004-02-16', 'MF', 10, 'Active'),
(8, 1, 'Патрик Мислович', '2001-05-28', 'MF', 23, 'Active'),
(9, 1, 'Евертон Бала', '1999-01-03', 'MF', 17, 'Active'),
(10, 1, 'Марин Петков', '2003-10-02', 'FW', 88, 'Active'),
(11, 1, 'Мустафа Сангаре', '1998-12-12', 'FW', 12, 'Active'),
(12, 2, 'Хендрик Бонман', '1994-01-19', 'GK', 1, 'Active'),
(13, 2, 'Йоел Андерсон', '1996-11-11', 'DF', 2, 'Active'),
(14, 2, 'Антон Недялков', '1993-04-30', 'DF', 3, 'Active'),
(15, 2, 'Оливие Вердон', '1995-10-05', 'DF', 24, 'Active'),
(16, 2, 'Диниш Алмейда', '1995-06-28', 'DF', 4, 'Active'),
(17, 2, 'Якуб Пьотровски', '1997-10-04', 'MF', 6, 'Active'),
(18, 2, 'Ивайло Чочев', '1993-02-18', 'MF', 18, 'Active'),
(19, 2, 'Филип Калоц', '2000-02-27', 'MF', 8, 'Active'),
(20, 2, 'Кайо Видал', '2000-11-04', 'FW', 11, 'Active'),
(21, 2, 'Бърнард Текпетей', '1997-09-03', 'FW', 37, 'Active'),
(22, 2, 'Ив Ерик Биле', '2004-12-24', 'FW', 29, 'Active'),
(23, 3, 'Густаво Бусато', '1990-08-23', 'GK', 1, 'Active'),
(24, 3, 'Юрген Матей', '1993-04-01', 'DF', 2, 'Active'),
(25, 3, 'Енес Махмутович', '1997-05-22', 'DF', 22, 'Active'),
(26, 3, 'Тибо Вион', '1993-12-11', 'DF', 15, 'Active'),
(27, 3, 'Брадли Де Нойер', '1997-11-13', 'DF', 5, 'Active'),
(28, 3, 'Амос Юга', '1992-12-08', 'MF', 21, 'Active'),
(29, 3, 'Йонатан Линдсет', '1996-02-25', 'MF', 7, 'Active'),
(30, 3, 'Тобиас Хайнц', '1998-07-13', 'MF', 14, 'Active'),
(31, 3, 'Маурисио Гарсес', '1997-07-16', 'FW', 10, 'Active'),
(32, 3, 'Дъкенс Назон', '1994-04-07', 'FW', 9, 'Active'),
(33, 3, 'Геферсон', '1994-07-13', 'DF', 3, 'Active'),
(34, 4, 'Иван Дюлгеров', '1999-07-15', 'GK', 25, 'Active'),
(35, 4, 'Васил Панайотов', '1990-07-16', 'MF', 71, 'Active'),
(36, 4, 'Живко Атанасов', '1991-02-03', 'DF', 3, 'Active'),
(37, 4, 'Даниел Димов', '1989-01-21', 'DF', 27, 'Active'),
(38, 4, 'Цветомир Панов', '1990-08-22', 'DF', 2, 'Active'),
(39, 4, 'Мазин Ахмед', '1997-03-10', 'MF', 8, 'Active'),
(40, 4, 'Виктор Попов', '2000-03-05', 'DF', 6, 'Active'),
(41, 4, 'Илиан Илиев', '1999-08-07', 'MF', 10, 'Active'),
(42, 4, 'Дуду', '1997-04-21', 'FW', 11, 'Active'),
(43, 4, 'Матеус Машадо', '1998-06-15', 'FW', 9, 'Active'),
(44, 4, 'Атанас Илиев', '1994-07-09', 'FW', 19, 'Active');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clubs`
--
ALTER TABLE `clubs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_player_club` (`club_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clubs`
--
ALTER TABLE `clubs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `players`
--
ALTER TABLE `players`
  ADD CONSTRAINT `fk_player_club` FOREIGN KEY (`club_id`) REFERENCES `clubs` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
