package Application.lobby;

import Application.room.CreateRoom;
import Application.room.Room;
import Application.room.RoomService;
import Application.room.name.RoomName;
import Application.room.name.RoomNameService;
import Application.room.topic.Topic;
import Application.room.topic.TopicService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.web.bind.annotation.*;

import java.util.List;

/**
 * Created by ivan on 12.4.2017 г..
 */

@RestController
public class LobbyController {
    @Autowired
    private RoomService roomService;
    @Autowired
    private TopicService topicService;
    @Autowired
    RoomNameService roomNameService;

    @RequestMapping(value = "/rest/rooms", method = RequestMethod.POST)
    @MessageMapping("/add-room")
    @SendTo("/rest/add-room")
    public Room createAndGiveRoom(CreateRoom roomParams) throws  Exception{
        if (topicService.findAll().size() == 0) {
            fillDataBaseWithTopics();
        }
        if (roomNameService.findAll().size() == 0) {
            fillDataBaseWithRoomNames();
        }

        Room r = new Room();
        r.setMaxPlayers(roomParams.getMaxPlayers());
        r.setName(roomNameService.findRandom());
        r.setTopic(topicService.findRandom());
        roomService.saveRoom(r);
        return r;
    }


    @RequestMapping("/lobby")
    public List<Room> getRooms() throws  Exception{
        return roomService.findAll();
    }

    private void fillDataBaseWithTopics() {
        byte[] image = new byte[0];
        Topic t = new Topic();
        t.setTopic("Cat");
        t.setCategory("Animal");
        t.setImage(image);
        topicService.saveTopic(t);
        Topic t1 = new Topic();
        t1.setTopic("Dog");
        t1.setCategory("Animal");
        t1.setImage(image);
        topicService.saveTopic(t1);
    }

    private void fillDataBaseWithRoomNames() {
        RoomName rn = new RoomName();
        rn.setRoomName("Qkata staq");
        roomNameService.saveRoomName(rn);
        RoomName rn1 = new RoomName();
        rn1.setRoomName("Ne6to");
        roomNameService.saveRoomName(rn1);
        RoomName rn2 = new RoomName();
        rn2.setRoomName("TUES");
        roomNameService.saveRoomName(rn2);
    }

}
