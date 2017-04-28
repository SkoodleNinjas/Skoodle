package Application;

import org.jboss.logging.Message;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.Mapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */

@Controller
public class LobbyController {
    @Autowired
    private RoomService roomService;

//    @MessageMapping("/lobby")
//    public Room addRoom(@PathVariable long maxPlayers, @PathVariable long playerId) {
//        Room room = new Room();
//        room.setMaxPlayers(maxPlayers);
//        return room;
//    }

    @MessageMapping("/hello")
    @SendTo("/topic/greetings")
    public Room getRooms(CreateRoom roomParams) throws  Exception{
        Room r = new Room();
        r.setMaxPlayers(roomParams.getMaxPlayers());
        roomService.saveRoom(r);
        return r;
    }


//    @RequestMapping("/")
//    public List<Room> getRooms() throws  Exception{
//        return roomService.findAll();
//    }

}
