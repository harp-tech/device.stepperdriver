#ifndef _QUICK_MOVEMENT_H_
#define _QUICK_MOVEMENT_H_
#include <avr/io.h>

// Define if not defined
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
	#define false 0
#endif

bool m1_quick_load_parameters (void);
bool m2_quick_load_parameters (void);
bool m1_quick_launch_movement (void);
bool m2_quick_launch_movement (void);
void m1_start_quick_movement (void);
void m2_start_quick_movement (void);

void m1_recalc_internal_paramenters (void);
bool m1_update_internal_variables (void);
bool m1_launch_quick_movement (void);
void m1_initiate_quick_movement (void);

#endif /* _QUICK_MOVEMENT_H_ */