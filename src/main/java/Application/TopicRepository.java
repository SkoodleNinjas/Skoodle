package Application;

import org.springframework.data.jpa.repository.JpaRepository;

/**
 * Created by nikolay on 4/26/17.
 */
public interface TopicRepository extends JpaRepository<Topic, Long> {
}